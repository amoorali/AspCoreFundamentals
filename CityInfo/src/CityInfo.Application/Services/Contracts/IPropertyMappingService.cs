using CityInfo.Application.Common.PropertyMapping;

namespace CityInfo.Application.Services.Contracts
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
    }
}