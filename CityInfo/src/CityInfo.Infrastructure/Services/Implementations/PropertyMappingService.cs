using CityInfo.Application.Common.PropertyMapping;
using CityInfo.Application.DTOs.City;
using CityInfo.Application.Services.Contracts;
using CityInfo.Domain.Entities;

namespace CityInfo.Infrastructure.Services.Implementations
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _cityPropertyMapping =
            new(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>{ "Id" }) },
                { "Name", new PropertyMappingValue(new List<string>{ "Name" }) },
                { "Description", new PropertyMappingValue(new List<string>{ "Description" }) }
            };

        private readonly IList<IPropertyMapping> _propertyMappings =
            new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<CityDto, City>(_cityPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings
                .OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
                return matchingMapping.First().MappingDictionary;

            throw new Exception($"Cannot find exact property mapping instance" +
                $"for <{typeof(TSource)},{typeof(TDestination)}>");
        }
    }
}
