using CityInfo.Application.DTOs;

namespace CityInfo.Application.Features.PointOfInterest.Results
{
    public record GetPointsOfInterestResult(
        bool Forbid,
        bool CityNotFound,
        IReadOnlyList<PointOfInterestDto>? Items
    );
}
