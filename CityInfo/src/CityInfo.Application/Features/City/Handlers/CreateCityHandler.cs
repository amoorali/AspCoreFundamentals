using CityInfo.Application.Common.Helpers;
using CityInfo.Application.DTOs.City;
using CityInfo.Application.Features.City.Commands;
using CityInfo.Application.Features.City.Results;
using CityInfo.Application.Repositories.Contracts;
using CityInfo.Application.Services.Contracts;
using Mapster;
using MediatR;

namespace CityInfo.Application.Features.City.Handlers
{
    public class CreateCityHandler :
        IRequestHandler<CreateCityCommand, CreateCityResult>
    {
        #region [ Fields ]
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region [ Constructor ]
        public CreateCityHandler(
            ICityRepository cityRepository,
            IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region [ Handler ]
        public async Task<CreateCityResult> Handle(
            CreateCityCommand request,
            CancellationToken cancellationToken)
        {
            var entity = request.Dto
                .Adapt<Domain.Entities.City>();

            await _cityRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var createdDto = entity
                .Adapt<CityDto>();

            var linkedResources = createdDto.ShapeData(null)
                as IDictionary<string, object?>;

            linkedResources.Add("links", request.Links);

            return new CreateCityResult(linkedResources);
        }
        #endregion
    }
}
