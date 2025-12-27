using CityInfo.Application.Features.PointOfInterest.Results;
using MediatR;

namespace CityInfo.Application.Features.PointOfInterest.Queries
{
    public record GetPointsOfInterestQuery(
        int CityId,
        string? CityName
    ) : IRequest<GetPointsOfInterestResult>;
}
