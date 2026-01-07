using CityInfo.Application.Common.Helpers;
using CityInfo.Application.DTOs.City;
using CityInfo.Application.Features.BaseImplementations;
using CityInfo.Application.Features.City.Queries;
using CityInfo.Application.Features.City.Results;
using CityInfo.Application.Services.Contracts;
using Mapster;
using MediatR;

namespace CityInfo.Application.Features.City.Handlers
{
    public class GetCityHandler : GeneralHandler,
        IRequestHandler<GetCityQuery, GetCityResult>
    {
        #region [ Constructor ]
        public GetCityHandler(
            IUnitOfWork unitOfWork,
            IMailService mailService,
            IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService)
            : base(unitOfWork, mailService, propertyCheckerService, propertyMappingService)
        {
        }
        #endregion

        #region [ Handler ]
        public async Task<GetCityResult> Handle(
            GetCityQuery request,
            CancellationToken cancellationToken)
        {
            if (!PropertyCheckerService.TypeHasProperties<CityDto>(request.Fields))
                return new GetCityResult(true, null);

            Domain.Entities.City? entity;

            if (request.IncludePointsOfInterest)
            {
                entity = await UnitOfWork.Cities.
                    GetCityWithPointsOfInterestAsync(request.CityId);
            }
            else
            {
                entity = await UnitOfWork.Cities
                    .GetCityWithoutPointsOfInterestAsync(request.CityId);
            }

            if (entity == null)
                return new GetCityResult(true, null);

            if (request.IncludePointsOfInterest)
            {
                var dtoWithPointsOfInterest = entity
                    .Adapt<CityDto>()
                    .ShapeData(request.Fields);

                return new GetCityResult(false, dtoWithPointsOfInterest);
            }

            var dtoWithoutPointsOfInterest = entity
                .Adapt<CityWithoutPointsOfInterestDto>()
                .ShapeData(request.Fields);

            return new GetCityResult(false, dtoWithoutPointsOfInterest);
        }
        #endregion
    }
}
