using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.APIs.Extensions
{
    public class SwaggerOptionsConfiguration : IConfigureOptions<SwaggerGenOptions>
    {
        #region [ Fields ]
        private readonly IApiVersionDescriptionProvider _provider;
        #endregion

        #region [ Constructure ]
        public SwaggerOptionsConfiguration(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
        #endregion

        #region [ Methods ]
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }
        #endregion

        #region [ Private ]
        private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var version = description.ApiVersion.ToString();

            var info = new OpenApiInfo()
            {
                Title = $"CityInfo.APIs {version}",
                Version = version
            };

            if (description.IsDeprecated)
            {
                info.Description += "This API version has been deprecated.";
            }

            return info;
        }
        #endregion
    }
}