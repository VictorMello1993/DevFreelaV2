using DevFreela.Domain.Repositories;
using DevFreela.Domain.Services.Auth;
using DevFreela.Domain.Services.MessageBus;
using DevFreela.Domain.Services.Payments;
using DevFreela.Domain.Services.SendMail.RedeemPassword;
using DevFreela.Infrastructure.Persistence.Repositories;
using DevFreela.Infrastructure.Services.Auth;
using DevFreela.Infrastructure.Services.MessageBus;
using DevFreela.Infrastructure.Services.Payments;
using DevFreela.Infrastructure.Services.SendMail.RedeemPassword;
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
            services.AddScoped<IMessageBusService, MessageBusService>();
            services.AddScoped<ISendEmailToRedeemPasswordService, SendEmailToRedeemPasswordService>();

            return services;
        }
    }
}
