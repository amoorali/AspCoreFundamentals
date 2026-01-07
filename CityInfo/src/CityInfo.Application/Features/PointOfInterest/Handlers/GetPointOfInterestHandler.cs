using CityInfo.Application.DTOs.PointOfInterest;
using CityInfo.Application.Features.BaseImplementations;
using CityInfo.Application.Features.PointOfInterest.Queries;
using CityInfo.Application.Features.PointOfInterest.Results;
using CityInfo.Application.Services.Contracts;
using Mapster;
using MediatR;

namespace CityInfo.Application.Features.PointOfInterest.Handlers
{
    public class GetPointOfInterestHandler : GeneralHandler,
        IRequestHandler<GetPointOfInterestQuery, GetPointOfInterestResult>
    {
        #region [ Constructor ]
        public GetPointOfInterestHandler(
            IUnitOfWork unitOfWork,
            IMailService mailService,
            IPropertyCheckerService propertyCheckerService)
            : base(unitOfWork, mailService, propertyCheckerService)
        {
        }
        #endregion

        #region [ Handler ]
        public async Task<GetPointOfInterestResult> Handle(
            GetPointOfInterestQuery request,
            CancellationToken cancellationToken)
        {
            if (!await UnitOfWork.Cities.CityExistsAsync(request.CityId))
                return new GetPointOfInterestResult(true, true, null);

            var pointOfInterest = await UnitOfWork.PointsOfInterest
                .GetPointOfInterestForCityAsync(request.CityId, request.PointOfInterestId);

            if (pointOfInterest == null)
                return new GetPointOfInterestResult(false, true, null);

            var dto = pointOfInterest
                .Adapt<PointOfInterestDto>();

            return new GetPointOfInterestResult(false, false, dto);
        }
        #endregion
    }
}
