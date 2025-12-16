using MapsterMapper;
using CityInfo.Application.Features.Commands.PointOfInterest;
using CityInfo.Application.Features.Results.PointOfInterest;
using CityInfo.Application.Services.Contracts;
using MediatR;
using CityInfo.Application.DTOs;

namespace CityInfo.Application.Features.Handlers.PointOfInterest
{
    public class CreatePointOfInterestHandler :
        IRequestHandler<CreatePointOfInterestCommand, CreatePointOfInterestResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePointOfInterestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CreatePointOfInterestResult> Handle(
            CreatePointOfInterestCommand request,
            CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Cities.CityExistsAsync(request.CityId))
                return new CreatePointOfInterestResult(true, null);

            var entity = _mapper.Map<Domain.Entities.PointOfInterest>(request.Dto);

            await _unitOfWork.Cities.AddPointOfInterestForCityAsync(request.CityId, entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var createdDto = _mapper.Map<PointOfInterestDto>(entity);

            return new CreatePointOfInterestResult(false, createdDto);
        }
    }
}
