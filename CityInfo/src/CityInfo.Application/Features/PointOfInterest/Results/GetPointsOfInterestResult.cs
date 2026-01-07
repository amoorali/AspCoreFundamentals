using CityInfo.Application.DTOs.PointOfInterest;

namespace CityInfo.Application.Features.PointOfInterest.Results
{
    #region [ Result Record ]
    public record GetPointsOfInterestResult(
        bool Forbid,
        IReadOnlyList<PointOfInterestDto>? Items
    );
    #endregion
}
