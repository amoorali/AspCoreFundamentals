using CityInfo.Application.Common;
using CityInfo.Application.Common.Exceptions;
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
            var parameters = request.CitiesResourceParameters;

            if (!PropertyMappingService.
                ValidMappingExistsFor<CityDto, Domain.Entities.City>(parameters.OrderBy))
            {
                throw new BadRequestException("Invalid sorting fields.");
            }

            if (!PropertyCheckerService.TypeHasProperties<CityWithoutPointsOfInterestDto>
                (parameters.Fields))
            {
                throw new BadRequestException("No cities found");
            }

            var query = UnitOfWork.Cities.QueryCities();

            #region [ Filter ]
            var name = parameters.Name?.Trim();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.Name == name);
            #endregion

            #region [ Search Query ]
            var searchQuery = parameters.SearchQuery?.Trim();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(c =>
                    c.Name.Contains(searchQuery) ||
                    !string.IsNullOrEmpty(c.Description) &&
                    c.Description.Contains(searchQuery));
            }
            #endregion

            #region [ Sorting ]
            if (!string.IsNullOrWhiteSpace(parameters.OrderBy) &&
                parameters.OrderBy.Equals("name", StringComparison.OrdinalIgnoreCase))
            {
                query = query.OrderBy(c => c.Name);
            }
            #endregion

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
