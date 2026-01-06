namespace CityInfo.Application.Common.PropertyMapping
{
    public class PropertyMapping<TSource, TDestination> : IPropertyMapping
    {
        #region [ Fields ]
        public Dictionary<string, PropertyMappingValue> MappingDictionary { get; private set; }
        #endregion

        #region [ Constructor ]
        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            MappingDictionary = mappingDictionary
                ?? throw new ArgumentNullException(nameof(mappingDictionary));
        }
        #endregion
    }
}
