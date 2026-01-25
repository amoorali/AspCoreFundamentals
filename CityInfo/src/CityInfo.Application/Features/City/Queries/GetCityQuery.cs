using CityInfo.Application.Features.City.Results;
using MediatR;

namespace CityInfo.Application.Features.City.Queries
{
    #region [ Query Record ]
    public record GetCityQuery(
        int CityId,
        bool IncludePointsOfInterest,
        string? Fields,
        string? MediaType
    ) : IRequest<GetCityResult>;
    #endregion
}

