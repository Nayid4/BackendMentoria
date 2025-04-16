using Mentoria.Services.Mentoring.Application.Common.Behaviors;
using Mentoria.Services.Mentoring.Application.Services;
using Mentoria.Services.Mentoring.Application.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Mentoria.Services.Mentoring.Application.Services
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            });

            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(BehaviorValidation<,>)
            );

            services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

            services.AddHttpContextAccessor();

            services.AddScoped<IAuthToken, AuthToken>();

            return services;
        }
    }
}
