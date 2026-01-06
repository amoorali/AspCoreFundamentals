namespace CityInfo.Application.Features.PointOfInterest.Results
{
    #region [ Result Record ]
    public record UpdatePointOfInterestResult(
        bool CityNotFound,
        bool PointOfInterestNotFound
    );
    #endregion
}
