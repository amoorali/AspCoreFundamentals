using CityInfo.Application.DTOs.PointOfInterest;
using CityInfo.Application.Features.PointOfInterest.Results;
using MediatR;

namespace CityInfo.Application.Features.PointOfInterest.Commands
{
    #region [ Command Record ]
    public record UpdatePointOfInterestCommand(
        int CityId,
        int PointOfInterestId,
        PointOfInterestForUpdateDto Dto
    ) : IRequest<UpdatePointOfInterestResult>;
    #endregion
}
