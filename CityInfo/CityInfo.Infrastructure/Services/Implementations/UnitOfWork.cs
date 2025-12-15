using CityInfo.Infrastructure.DbContexts;
using CityInfo.Infrastructure.Repositories.Contracts;
using CityInfo.Infrastructure.Repositories.Implementations;
using CityInfo.Infrastructure.Services.Contracts;

namespace CityInfo.Infrastructure.Services.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        #region [ Fields ]
        private readonly CityInfoContext _context;
        public ICityRepository Cities { get; private set; }
        public IPointOfInterestRepository PointsOfInterest { get; private set; }
        #endregion

        #region [ Constructor ]
        public UnitOfWork(CityInfoContext context)
        {
            _context = context;
            Cities = new CityRepository(_context);
            PointsOfInterest = new PointOfInterestRepository(_context);
        }
        #endregion

        #region [ Methods ]
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
            _context.DisposeAsync();
        }
        #endregion
    }
}
