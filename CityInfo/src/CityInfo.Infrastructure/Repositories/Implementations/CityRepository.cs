using CityInfo.Application.Repositories.Contracts;
using CityInfo.Domain.Entities;
using CityInfo.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Infrastructure.Repositories.Implementations
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        #region [ Constructor ]
        public CityRepository(CityInfoContext context)
            : base(context)
        {
        }
        #endregion

        #region [ Methods ]
        public IQueryable<City> QueryCities()
            => Context.Cities.AsNoTracking();

        public async Task<City?> GetCityWithPointsOfInterestAsync(int cityId)
        {
            return await Context.Cities
                .Include(c => c.PointsOfInterest)
                .FirstOrDefaultAsync(c => c.Id == cityId);
        }

        public async Task<City?> GetCityWithoutPointsOfInterestAsync(int cityId)
        {
            return await Context.Cities
                .FirstOrDefaultAsync(c => c.Id == cityId);
        }
        #endregion
    }
}
