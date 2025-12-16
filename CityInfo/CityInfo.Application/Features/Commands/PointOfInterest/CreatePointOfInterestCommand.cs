using CityInfo.Application.DTOs;
using CityInfo.Application.Features.Results.PointOfInterest;
using MediatR;

namespace CityInfo.Application.Features.Commands.PointOfInterest
{
    public record CreatePointOfInterestCommand (
        int CityId,
        PointOfInterestForCreationDto Dto
    ) : IRequest<CreatePointOfInterestResult>;
}
