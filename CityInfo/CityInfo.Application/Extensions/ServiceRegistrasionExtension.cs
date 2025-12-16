using Microsoft.Extensions.DependencyInjection;
using Mapster;

namespace CityInfo.Application.Extensions
{
    public static class ServiceRegistrasionExtension
    {
        public static void ConfigureApplicationLayer(this IServiceCollection services)
        {
            #region [ Mapster ]
            var config = new TypeAdapterConfig();

            config.Scan(typeof(ServiceRegistrasionExtension).Assembly);

            services.AddMapster();
            #endregion

            #region [ MediatR ]
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(ServiceRegistrasionExtension).Assembly)
            });
            #endregion
        }
    }
}
