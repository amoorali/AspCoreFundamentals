using CityInfo.Application.Common;

namespace CityInfo.Application.Features.City.Results
{
    #region [ Result Record ]
    public record GetCitiesResult(
        object? Item,
        PaginationMetadata PaginationMetadata
    );
    #endregion
}
