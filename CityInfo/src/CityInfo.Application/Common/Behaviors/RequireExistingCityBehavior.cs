using CityInfo.Application.Common.Contracts;
using CityInfo.Application.Common.Exceptions;
using CityInfo.Application.Repositories.Contracts;
using MediatR;

namespace CityInfo.Application.Common.Behaviors
{
    public sealed class RequireExistingCityBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        #region [ Fields ]
        private readonly ICityRepository _cityRepository;
        #endregion

        #region [ Constructor ]
        public RequireExistingCityBehavior(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        #endregion

        #region [ Handler ]
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (request is IRequireExisitingCity requireExistingCity)
            {
                var exists = await _cityRepository
                    .ExistsAsync(requireExistingCity.CityId);

                if (!exists)
                    throw new NotFoundException($"City {requireExistingCity.CityId} not found.");
            }

            return await next(cancellationToken);
        }
        #endregion
    }
}
