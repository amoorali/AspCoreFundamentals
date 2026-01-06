using CityInfo.Application.Common;
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
    public class GetCitiesHandler : GeneralHandler,
        IRequestHandler<GetCitiesQuery, GetCitiesResult>
    {
        #region [ Constructor ]
        public GetCitiesHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMailService mailService,
            IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService)
            : base(unitOfWork, mapper, mailService, propertyCheckerService, propertyMappingService)
        {
        }
        #endregion

        #region [ Handler ]
        public async Task<GetCitiesResult> Handle(
            GetCitiesQuery request,
            CancellationToken cancellationToken)
        {
            if (!PropertyCheckerService.TypeHasProperties<CityWithoutPointsOfInterestDto>
                (request.CitiesResourceParameters.Fields))
            {
                return new GetCitiesResult(null, false, false, null);
            }

            var parameters = request.CitiesResourceParameters;
            var query = UnitOfWork.Cities.QueryCities();

            if (!string.IsNullOrEmpty(parameters.Name))
            {
                parameters.Name = parameters.Name.Trim();
                query = query.Where(c => c.Name == parameters.Name);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                var search = parameters.SearchQuery.Trim();
                query = query.Where(c => 
                    c.Name.Contains(search) ||
                    !string.IsNullOrEmpty(c.Description) &&
                    c.Description.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(parameters.OrderBy) &&
                parameters.OrderBy.ToLowerInvariant() == "name")
            {
                query = query.OrderBy(c => c.Name);
            }

            var pagedEntities = await PagedList<Domain.Entities.City>.CreateAsync(
                query,
                parameters.PageNumber,
                parameters.PageSize);

            var paginationMetadata = new PaginationMetadata(
                pagedEntities.TotalCount,
                pagedEntities.PageSize,
                pagedEntities.CurrentPage,
                pagedEntities.TotalPages);

            var dtos = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(pagedEntities.Items)
                             .ShapeData(parameters.Fields);

            return new GetCitiesResult(
                dtos,
                pagedEntities.HasPrevious,
                pagedEntities.HasNext,
                paginationMetadata);
        }
        #endregion
    }
}
