using CityInfo.Application.Features.PointOfInterest.Results;
using MediatR;

namespace CityInfo.Application.Features.PointOfInterest.Queries
{
    public record GetPointOfInterestQuery(
        int CityId,
        int PointOfInterestId
    ) : IRequest<GetPointOfInterestResult>;
}
