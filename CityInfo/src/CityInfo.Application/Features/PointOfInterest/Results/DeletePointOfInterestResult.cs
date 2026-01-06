namespace CityInfo.Application.Features.PointOfInterest.Results
{
    #region [ Result Record ]
    public record DeletePointOfInterestResult(
        bool CityNotFound,
        bool PointOfInterestNotFound
    );
    #endregion
}
