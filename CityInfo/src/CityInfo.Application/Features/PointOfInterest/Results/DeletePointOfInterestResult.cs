namespace CityInfo.Application.Features.PointOfInterest.Results
{
    #region [ Result Record ]
    public record DeletePointOfInterestResult(
        bool PointOfInterestNotFound
    );
    #endregion
}
