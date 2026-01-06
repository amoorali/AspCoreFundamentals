namespace CityInfo.Application.Common.PropertyMapping
{
    public class PropertyMappingValue
    {
        #region [ Fields ]
        public IEnumerable<string> DestinationProperties { get; private set; }
        public bool Revert { get; private set; }
        #endregion

        #region [ Constructor ]
        public PropertyMappingValue(IEnumerable<string> destinationProperties,
            bool revert = false)
        {
            DestinationProperties = destinationProperties
                ?? throw new ArgumentNullException(nameof(destinationProperties));
            Revert = revert;
        }
        #endregion
    }
}
