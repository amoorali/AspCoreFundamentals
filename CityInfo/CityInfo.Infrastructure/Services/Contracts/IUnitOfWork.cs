using CityInfo.Infrastructure.Repositories.Contracts;

namespace CityInfo.Infrastructure.Services.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository Cities { get; }
        IPointOfInterestRepository PointsOfInterest { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
