using CityInfo.Domain.Entities;

namespace CityInfo.Application.Repositories.Contracts
{
    public interface ICityRepository : IRepository<City>
    {
        IQueryable<City> QueryCities();
        Task<City?> GetCityWithPointsOfInterestAsync(int cityId);
        Task<City?> GetCityWithoutPointsOfInterestAsync(int cityId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task<bool> CityNameMatchesCityIdAsync(string? cityName, int cityId);
    }
}
