namespace CityInfo.Application.Features.City.Results
{
    #region [ Result Record ]
    public record GetCityResult(
        bool NotFound,
        object? Item
    );
    #endregion
}
