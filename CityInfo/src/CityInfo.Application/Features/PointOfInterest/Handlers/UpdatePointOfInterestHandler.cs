using CityInfo.Application.Features.BaseImplementations;
using CityInfo.Application.Features.PointOfInterest.Commands;
using CityInfo.Application.Features.PointOfInterest.Results;
using CityInfo.Application.Services.Contracts;
using Mapster;
using MediatR;

namespace CityInfo.Application.Features.PointOfInterest.Handlers
{
    public class UpdatePointOfInterestHandler : GeneralHandler,
        IRequestHandler<UpdatePointOfInterestCommand, UpdatePointOfInterestResult>
    {
        #region [ Constructor ]
        public UpdatePointOfInterestHandler(
            IUnitOfWork unitOfWork,
            IMailService mailService,
            IPropertyCheckerService propertyCheckerService)
            : base(unitOfWork, mailService, propertyCheckerService)
        {
        }
        #endregion

        #region [ Handler ]
        public async Task<UpdatePointOfInterestResult> Handle(
            UpdatePointOfInterestCommand request,
            CancellationToken cancellationToken)
        {
            if (!await UnitOfWork.Cities.CityExistsAsync(request.CityId))
                return new UpdatePointOfInterestResult(true, true);

            var entity = await UnitOfWork.PointsOfInterest
                .GetPointOfInterestForCityAsync(request.CityId, request.PointOfInterestId);

            if (entity == null)
                return new UpdatePointOfInterestResult(false, true);

            request.Dto.Adapt(entity);

            await UnitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdatePointOfInterestResult(false, false);
        }
        #endregion
    }
}
