using CityInfo.Application.Common.Helpers;
using CityInfo.Application.DTOs.City;
using CityInfo.Application.Features.BaseImplementations;
using CityInfo.Application.Features.City.Queries;
using CityInfo.Application.Features.City.Results;
using CityInfo.Application.Services.Contracts;
using MapsterMapper;
using MediatR;

namespace CityInfo.Application.Features.City.Handlers
{
    public class GetCityHandler : GeneralHandler,
        IRequestHandler<GetCityQuery, GetCityResult>
    {
        public GetCityHandler(IUnitOfWork unitOfWork, IMapper mapper, IMailService mailService)
            : base(unitOfWork, mapper, mailService)
        {
        }

        public async Task<GetCityResult> Handle(
            GetCityQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await UnitOfWork.Cities
                .GetCityAsync(request.CityId, request.IncludePointsOfInterest);

            if (entity == null)
                return new GetCityResult(true, null);

            if (request.IncludePointsOfInterest)
            {
                var dtoWithPointsOfInterest = Mapper.Map<CityDto>(entity)
                    .ShapeData(request.Fields);
                return new GetCityResult(false, dtoWithPointsOfInterest);
            }

            var dtoWithoutPointsOfInterest = Mapper.Map<CityWithoutPointsOfInterestDto>(entity)
                .ShapeData(request.Fields);

            return new GetCityResult(false, dtoWithoutPointsOfInterest);
        }
    }
}
