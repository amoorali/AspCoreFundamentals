using Microsoft.Extensions.DependencyInjection;
using Mapster;

namespace CityInfo.Application.Extensions
{
    public static class ServiceRegistrasionExtension
    {
        public static void ConfigureApplicationLayer(this IServiceCollection services)
        {
            #region [ Mapster ]
            TypeAdapterConfig.GlobalSettings.Scan(typeof(ServiceRegistrasionExtension).Assembly);
            TypeAdapterConfig.GlobalSettings.Compile();
            #endregion

            #region [ MediatR ]
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(ServiceRegistrasionExtension).Assembly);
            });
            #endregion
        }
    }
}
