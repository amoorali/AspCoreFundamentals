using CityInfo.Application.DTOs.PointOfInterest;
using CityInfo.Application.Features.BaseImplementations;
using CityInfo.Application.Features.PointOfInterest.Queries;
using CityInfo.Application.Features.PointOfInterest.Results;
using CityInfo.Application.Services.Contracts;
using Mapster;
using MediatR;

namespace CityInfo.Application.Features.PointOfInterest.Handlers
{
    public class GetPointsOfInterestHandler : GeneralHandler,
        IRequestHandler<GetPointsOfInterestQuery, GetPointsOfInterestResult>
    {
        #region [ Constructor ]
        public GetPointsOfInterestHandler(
            IUnitOfWork unitOfWork,
            IMailService mailService,
            IPropertyCheckerService propertyCheckerService)
            : base(unitOfWork, mailService, propertyCheckerService)
        {
        }
        #endregion

        #region [ Handler ]
        public async Task<GetPointsOfInterestResult> Handle(
            GetPointsOfInterestQuery request,
            CancellationToken cancellationToken)
        {
            if (!await UnitOfWork.Cities.CityNameMatchesCityIdAsync(request.CityName, request.CityId))
                return new GetPointsOfInterestResult(true, true, null);

            if (!await UnitOfWork.Cities.CityExistsAsync(request.CityId))
                return new GetPointsOfInterestResult(false, true, null);

            var pointsOfInterest = await UnitOfWork.PointsOfInterest
                .GetPointsOfInterestForCityAsync(request.CityId);

            var entities = pointsOfInterest
                .Adapt<IReadOnlyList<PointOfInterestDto>>();

            return new GetPointsOfInterestResult(false, false, entities);
        }
        #endregion
    }
}
