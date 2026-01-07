using CityInfo.Application.DTOs.PointOfInterest;
using CityInfo.Application.Features.PointOfInterest.Commands;
using CityInfo.Application.Features.PointOfInterest.Results;
using CityInfo.Application.Repositories.Contracts;
using CityInfo.Application.Services.Contracts;
using Mapster;
using MediatR;

namespace CityInfo.Application.Features.PointOfInterest.Handlers
{
    public class CreatePointOfInterestHandler :
        IRequestHandler<CreatePointOfInterestCommand, CreatePointOfInterestResult>
    {
        #region [ Fields ]
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region [ Constructor ]
        public CreatePointOfInterestHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region [ Handler ]
        public async Task<CreatePointOfInterestResult> Handle(
            CreatePointOfInterestCommand request,
            CancellationToken cancellationToken)
        {
            var entity = request.Dto
                .Adapt<Domain.Entities.PointOfInterest>();

            await _cityRepository.AddPointOfInterestForCityAsync(request.CityId, entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var createdDto = entity
                .Adapt<PointOfInterestDto>();

            return new CreatePointOfInterestResult(createdDto);
        }
        #endregion
    }
}
