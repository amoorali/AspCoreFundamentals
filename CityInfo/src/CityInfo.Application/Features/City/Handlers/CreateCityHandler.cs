using CityInfo.Application.Common.Contracts;
using CityInfo.Application.Common.Exceptions;
using CityInfo.Application.Common.Helpers;
using CityInfo.Application.DTOs.City;
using CityInfo.Application.Features.City.Commands;
using CityInfo.Application.Features.City.Results;
using CityInfo.Application.Repositories.Contracts;
using CityInfo.Application.Services.Contracts;
using Mapster;
using MediatR;
using System.Net.Http.Headers;

namespace CityInfo.Application.Features.City.Handlers
{
    public class CreateCityHandler :
        IRequestHandler<CreateCityCommand, CreateCityResult>
    {
        #region [ Fields ]
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityLinkService _cityLinkService;
        #endregion

        #region [ Constructor ]
        public CreateCityHandler(
            ICityRepository cityRepository,
            IUnitOfWork unitOfWork,
            ICityLinkService cityLinkService)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
            _cityLinkService = cityLinkService;
        }
        #endregion

        #region [ Handler ]
        public async Task<CreateCityResult> Handle(
            CreateCityCommand request,
            CancellationToken cancellationToken)
        {
            if (!MediaTypeHeaderValue.TryParse(request.MediaType, out var parsedMediaType))
                throw new BadRequestException("Accept header media type value is not a valid media type.");

            var entity = request.Dto
                .Adapt<Domain.Entities.City>();

            await _cityRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var createdDto = entity
                .Adapt<CityDto>();

            if (parsedMediaType.MediaType!.Equals("application/vnd.marvin.hateoas+json",
                StringComparison.OrdinalIgnoreCase))
            {
                var linkedResources = createdDto.ShapeData(null)
                    as IDictionary<string, object?>;

                var links = _cityLinkService.CreateLinksForCity(createdDto.Id, null);

                linkedResources.Add("links", links);

                return new CreateCityResult(Item: linkedResources);
            }

            return new CreateCityResult(createdDto);
        }
        #endregion
    }
}
