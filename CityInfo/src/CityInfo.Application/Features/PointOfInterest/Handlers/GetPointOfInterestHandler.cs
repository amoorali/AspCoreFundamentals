using CityInfo.Application.DTOs.PointOfInterest;
using CityInfo.Application.Features.PointOfInterest.Queries;
using CityInfo.Application.Features.PointOfInterest.Results;
using CityInfo.Application.Repositories.Contracts;
using Mapster;
using MediatR;

namespace CityInfo.Application.Features.PointOfInterest.Handlers
{
    public class GetPointOfInterestHandler : IRequestHandler<GetPointOfInterestQuery, GetPointOfInterestResult>
    {
        #region [ Fields ]
        private readonly IPointOfInterestRepository _pointOfInterestRepository;
        #endregion

        #region [ Constructor ]
        public GetPointOfInterestHandler(
            IPointOfInterestRepository pointOfInterestRepository)
        {
            _pointOfInterestRepository = pointOfInterestRepository;
        }
        #endregion

        #region [ Handler ]
        public async Task<GetPointOfInterestResult> Handle(
            GetPointOfInterestQuery request,
            CancellationToken cancellationToken)
        {
            var pointOfInterest = await _pointOfInterestRepository
                .GetPointOfInterestForCityAsync(request.CityId, request.PointOfInterestId);

            if (pointOfInterest == null)
                return new GetPointOfInterestResult(true, null);

            var dto = pointOfInterest
                .Adapt<PointOfInterestDto>();

            return new GetPointOfInterestResult(false, dto);
        }
        #endregion
    }
}
