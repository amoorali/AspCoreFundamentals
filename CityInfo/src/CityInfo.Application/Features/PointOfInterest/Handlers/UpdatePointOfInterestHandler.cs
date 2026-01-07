using CityInfo.Application.Features.PointOfInterest.Commands;
using CityInfo.Application.Features.PointOfInterest.Results;
using CityInfo.Application.Repositories.Contracts;
using CityInfo.Application.Services.Contracts;
using Mapster;
using MediatR;

namespace CityInfo.Application.Features.PointOfInterest.Handlers
{
    public class UpdatePointOfInterestHandler : IRequestHandler<UpdatePointOfInterestCommand, UpdatePointOfInterestResult>
    {
        #region [ Fields ]
        private readonly IPointOfInterestRepository _pointOfInterestRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region [ Constructor ]
        public UpdatePointOfInterestHandler(
            IPointOfInterestRepository pointOfInterestRepository,
            IUnitOfWork unitOfWork)
        {
            _pointOfInterestRepository = pointOfInterestRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region [ Handler ]
        public async Task<UpdatePointOfInterestResult> Handle(
            UpdatePointOfInterestCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _pointOfInterestRepository
                .GetPointOfInterestForCityAsync(request.CityId, request.PointOfInterestId);

            if (entity == null)
                return new UpdatePointOfInterestResult(true);

            request.Dto.Adapt(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdatePointOfInterestResult(false);
        }
        #endregion
    }
}
