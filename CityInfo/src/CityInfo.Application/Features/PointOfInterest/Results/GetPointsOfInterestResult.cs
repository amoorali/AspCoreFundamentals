using CityInfo.Application.DTOs.PointOfInterest;

namespace CityInfo.Application.Features.PointOfInterest.Results
{
    #region [ Result Record ]
    public record GetPointsOfInterestResult(
        bool Forbid,
        bool CityNotFound,
        IReadOnlyList<PointOfInterestDto>? Items
    );
    #endregion
}
