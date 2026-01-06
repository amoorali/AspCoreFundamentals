using CityInfo.Application.Common;
using System.Dynamic;

namespace CityInfo.Application.Features.City.Results
{
    #region [ Result Record ]
    public record GetCitiesResult(
        IEnumerable<ExpandoObject> Items,
        bool HasPreviousPage,
        bool HasNextPage,
        PaginationMetadata PaginationMetadata
    );
    #endregion
}
