using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.DeleteUser;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Commands.UpdateUser;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Validators;
using DevFreela.Domain.Repositories;
using DevFreela.Domain.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
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
            //Configura��o da aplica��o para definir o hor�rio da requisi��o
            //services.Configure<OpeningTimeOption>(Configuration.GetSection("OpeningTime"));

            //Configura��o de banco de dados com SQL Server
            var connectionString = Configuration.GetConnectionString("DevFreelaV2SQLServer");
            services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));

            //Configura��o de banco de dados em mem�ria
            //services.AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase("DevFreela"));

            //-----------------------------------------------------Configura��o de inje��o de depend�ncia para acesso a banco de dados-----------------------------------------------------------------------------------------------------------------------
            //services.AddSingleton<DevFreelaDbContext>();            

            //services.AddScoped<IProjectService, ProjectService>();
            //services.AddScoped<ISkillService, SkillService>();
            //services.AddScoped<IUserService, UserService>();
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            services.AddMediatR(typeof(CreateProjectCommand));            

            //Teste do mecanismo de inje��o de depend�ncia para verificar se o estado do objeto foi alterado para cada requisi��o atrav�s do padr�o Singleton (uma inst�ncia por aplica��o)
            //services.AddSingleton<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" });

            //Teste do mecanismo de inje��o de depend�ncia para verificar se o estado do objeto foi alterado para cada requisi��o atrav�s do padr�o Scoped (uma inst�ncia por requisi��o)
            //services.AddScoped<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" });

            //Adcionando configura��es do repository
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Configura��es de autentica��o e autoriza��o do usu�rio
            services.AddScoped<IAuthService, AuthService>();

            services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter))) //Configurando filtros de valida��o
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateSkillCommandValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreelaV2.API", Version = "v1" });

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
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //Esquema de autentica��o padr�o: Bearer
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
