using CityInfo.Application.DTOs.PointOfInterest;

namespace CityInfo.Application.Features.PointOfInterest.Results
{
    #region [ Result Record ]
    public record GetPointOfInterestResult(
        bool PointOfInterestNotFound,
        PointOfInterestDto? Item
    );
    #endregion
}
