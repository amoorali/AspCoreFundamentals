using CityInfo.Application.Services.Contracts;
using MediatR;
using CityInfo.Application.Features.PointOfInterest.Commands;
using CityInfo.Application.Features.PointOfInterest.Results;
using CityInfo.Application.Features.BaseImplementations;
using CityInfo.Application.DTOs.PointOfInterest;
using Mapster;

namespace CityInfo.Application.Features.PointOfInterest.Handlers
{
    public class CreatePointOfInterestHandler : GeneralHandler,
        IRequestHandler<CreatePointOfInterestCommand, CreatePointOfInterestResult>
    {
        #region [ Constructor ]
        public CreatePointOfInterestHandler(
            IUnitOfWork unitOfWork,
            IMailService mailService,
            IPropertyCheckerService propertyCheckerService)
            : base(unitOfWork, mailService, propertyCheckerService)
        {
        }
        #endregion

        #region [ Handler ]
        public async Task<CreatePointOfInterestResult> Handle(
            CreatePointOfInterestCommand request,
            CancellationToken cancellationToken)
        {
            if (!await UnitOfWork.Cities.CityExistsAsync(request.CityId))
                return new CreatePointOfInterestResult(true, null);

            var entity = request.Dto
                .Adapt<Domain.Entities.PointOfInterest>();

            await UnitOfWork.Cities.AddPointOfInterestForCityAsync(request.CityId, entity);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            var createdDto = entity
                .Adapt<PointOfInterestDto>();

            return new CreatePointOfInterestResult(false, createdDto);
        }
        #endregion
    }
}
