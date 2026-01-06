using CityInfo.Application.Common.ResourceParameters;
using CityInfo.Application.Features.City.Results;
using MediatR;

namespace CityInfo.Application.Features.City.Queries
{
    #region [ Query Record ]
    public record GetCitiesQuery(
        CitiesResourceParameters CitiesResourceParameters
    ) : IRequest<GetCitiesResult>;
    #endregion
}
