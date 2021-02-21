using DevFreela.Application.Commands.CreateProject;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreelaV2.API", Version = "v1" });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
