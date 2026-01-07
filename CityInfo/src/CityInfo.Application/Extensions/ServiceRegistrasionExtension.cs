using Microsoft.Extensions.DependencyInjection;
using Mapster;
using MediatR;
using CityInfo.Application.Common.Behaviors;

namespace CityInfo.Application.Extensions
{
    public static class ServiceRegistrasionExtension
    {
        public static void ConfigureApplicationLayer(this IServiceCollection services)
        {
            #region [ Mapster ]
            TypeAdapterConfig.GlobalSettings.Scan(typeof(ServiceRegistrasionExtension).Assembly);
            #endregion

            #region [ MediatR ]
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(ServiceRegistrasionExtension).Assembly);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequireExistingCityBehavior<,>));
            #endregion
        }
    }
}
