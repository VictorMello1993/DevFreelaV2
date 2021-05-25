using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Consumers;
using DevFreela.Application.Validators;
using DevFreela.Infrastructure.Persistence;
using DevFreelaV2.API.Extensions;
using DevFreelaV2.API.Filters;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace DevFreelaV2.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configuração da aplicação para definir o horário da requisição
            //services.Configure<OpeningTimeOption>(Configuration.GetSection("OpeningTime"));

            //Configuração de banco de dados com SQL Server
            var connectionString = Configuration.GetConnectionString("DevFreelaV2SQLServer");
            services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));

            services.AddHostedService<PaymentApprovedConsumer>();

            //Configuração de banco de dados em memória
            //services.AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase("DevFreela"));

            //-----------------------------------------------------Configuração de injeção de dependência para acesso a banco de dados-----------------------------------------------------------------------------------------------------------------------
            //services.AddSingleton<DevFreelaDbContext>();            

            //services.AddScoped<IProjectService, ProjectService>();
            //services.AddScoped<ISkillService, SkillService>();
            //services.AddScoped<IUserService, UserService>();
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            services.AddHttpClient();

            services.AddInfrastructure();

            //Configuração da biblioteca MediatR (padrão CQRS)
            services.AddMediatR(typeof(CreateProjectCommand));

            //Teste do mecanismo de injeção de dependência para verificar se o estado do objeto foi alterado para cada requisição através do padrão Singleton (uma instância por aplicação)
            //services.AddSingleton<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" });

            //Teste do mecanismo de injeção de dependência para verificar se o estado do objeto foi alterado para cada requisição através do padrão Scoped (uma instância por requisição)
            //services.AddScoped<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" });                        

            services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter))) //Configurando filtros de validação
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateSkillCommandValidator>()); //Configuração do Fluent Validation

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DevFreelaV2.API",
                    Version = "v1",                    
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header usando o esquema Bearer."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}
                     }
                 });                
            });

            services
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //Esquema de autenticação padrão: Bearer
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,

                      ValidIssuer = Configuration["Jwt:Issuer"],
                      ValidAudience = Configuration["Jwt:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                  };
              });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevFreelaV2.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
