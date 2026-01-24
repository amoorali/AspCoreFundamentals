namespace CityInfo.Application.Features.City.Results
{
    #region [ Result Record ]
    public record CreateCityResult(
        IDictionary<string, object?>? LinkedResources
    );
    #endregion
}
