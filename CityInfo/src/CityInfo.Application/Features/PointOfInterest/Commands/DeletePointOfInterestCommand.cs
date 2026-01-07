using CityInfo.Application.Common.Contracts;
using CityInfo.Application.Features.PointOfInterest.Results;
using MediatR;

namespace CityInfo.Application.Features.PointOfInterest.Commands
{
    #region [ Command Record ]
    public record DeletePointOfInterestCommand(
        int CityId,
        int PointOfInterestId
    ) : IRequest<DeletePointOfInterestResult>, IRequireExisitingCity;
    #endregion
}
