using CityInfo.Application.DTOs.City;
using CityInfo.Application.Features.City.Results;
using MediatR;

namespace CityInfo.Application.Features.City.Commands
{
    #region [ Command Record ]
    public record CreateCityCommand(
        CityForCreationDto Dto,
        string? MediaType
    ) : IRequest<CreateCityResult>;
    #endregion
}
