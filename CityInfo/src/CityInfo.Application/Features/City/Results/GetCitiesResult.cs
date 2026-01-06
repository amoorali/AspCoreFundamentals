using CityInfo.Application.Common;
using CityInfo.Application.DTOs.City;
using System.Dynamic;

namespace CityInfo.Application.Features.City.Results
{
    public record GetCitiesResult(
        IEnumerable<ExpandoObject> Items,
        bool HasPreviousPage,
        bool HasNextPage,
        PaginationMetadata PaginationMetadata
    );
}
