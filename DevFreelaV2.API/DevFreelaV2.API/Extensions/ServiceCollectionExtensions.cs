using DevFreela.Domain.Repositories;
using DevFreela.Domain.Services.Auth;
using DevFreela.Domain.Services.Payments;
using DevFreela.Infrastructure.Persistence.Repositories;
using DevFreela.Infrastructure.Services.Auth;
using DevFreela.Infrastructure.Services.Payments;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreelaV2.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {            
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPaymentService, PaymentService>();
            //services.AddScoped<IMessageBusService, MessageBusService>();

            return services;
        }
    }
}
