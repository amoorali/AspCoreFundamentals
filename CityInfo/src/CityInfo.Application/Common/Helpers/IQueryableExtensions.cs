using CityInfo.Application.Common.Exceptions;
using CityInfo.Application.Common.PropertyMapping;
using System.Linq.Dynamic.Core;
using System.Text;

namespace CityInfo.Application.Common.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(
            this IQueryable<T> source,
            string orderBy,
            Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(mappingDictionary);

            if (string.IsNullOrWhiteSpace(orderBy))
                return source;

            var orderByString = new StringBuilder();
            var orderByAfterSplit = orderBy.Split(',');

            foreach (var orderByClause in  orderByAfterSplit)
            {
                var trimmedOrderByClause = orderByClause.Trim();

                // if the sort option ends with desc, we order descending, otherwise ascending
                var orderDescending = trimmedOrderByClause.EndsWith(" desc", StringComparison.OrdinalIgnoreCase);

                // remove " asc" or " desc" from the orderBy clause, so we get the property name
                // to look for in the mapping dictionary
                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

                // find the matching property
                if (!mappingDictionary.ContainsKey(propertyName))
                    throw new BadRequestException($"Invalid orderBy field: '{propertyName}'.");

                // get the PropertyMappingValue
                var propertyMappingValue = mappingDictionary[propertyName];
                
                if (propertyMappingValue == null )
                    throw new ArgumentNullException(nameof(propertyMappingValue));

                // revert sort order if necessary
                if (propertyMappingValue.Revert)
                    orderDescending = !orderDescending;


                // run through the property names
                foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
                {
                    orderByString.Append((orderByString.Length == 0 ? string.Empty : ", "))
                                 .Append(destinationProperty)
                                 .Append((orderDescending ? " descending" : " ascending"));
                }
            }

            return source.OrderBy(orderByString.ToString());
        }
    }
}
