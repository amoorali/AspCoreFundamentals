using CityInfo.Application.Features.PointOfInterest.Results;
using MediatR;

namespace CityInfo.Application.Features.PointOfInterest.Queries
{
    #region [ Query Record ]
    public record GetPointOfInterestQuery(
        int CityId,
        int PointOfInterestId
    ) : IRequest<GetPointOfInterestResult>;
    #endregion
}
