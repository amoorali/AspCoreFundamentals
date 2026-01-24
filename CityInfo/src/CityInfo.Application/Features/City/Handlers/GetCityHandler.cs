using CityInfo.Application.Common.Helpers;
using CityInfo.Application.DTOs.City;
using CityInfo.Application.Features.City.Queries;
using CityInfo.Application.Features.City.Results;
using CityInfo.Application.Repositories.Contracts;
using CityInfo.Application.Services.Contracts;
using Mapster;
using MediatR;

namespace CityInfo.Application.Features.City.Handlers
{
    public class GetCityHandler : IRequestHandler<GetCityQuery, GetCityResult>
    {
        #region [ Fields ]
        private readonly ICityRepository _cityRepository;
        private readonly IPropertyCheckerService _propertyCheckerService;
        #endregion

        #region [ Constructor ]
        public GetCityHandler(
            ICityRepository cityRepository,
            IPropertyCheckerService propertyCheckerService)
        {
            _cityRepository = cityRepository;
            _propertyCheckerService = propertyCheckerService;
        }
        #endregion

        #region [ Handler ]
        public async Task<GetCityResult> Handle(
            GetCityQuery request,
            CancellationToken cancellationToken)
        {
            if (!_propertyCheckerService.TypeHasProperties<CityDto>(request.Fields))
                return new GetCityResult(true, null);

            Domain.Entities.City? entity;

            if (request.IncludePointsOfInterest)
            {
                entity = await _cityRepository.
                    GetCityWithPointsOfInterestAsync(request.CityId);
            }
            else
            {
                entity = await _cityRepository
                    .GetCityWithoutPointsOfInterestAsync(request.CityId);
            }

            if (entity == null)
                return new GetCityResult(true, null);

            IDictionary<string, object?> linkedResources;

            if (request.IncludePointsOfInterest)
            {
                linkedResources = entity
                    .Adapt<CityDto>()
                    .ShapeData(request.Fields);
            }
            else
            {
                linkedResources = entity
                    .Adapt<CityWithoutPointsOfInterestDto>()
                    .ShapeData(request.Fields);
            }

            linkedResources.Add("links", request.Links);

            return new GetCityResult(false, linkedResources);
        }
        #endregion
    }
}
