using CityInfo.Application.DTOs;

namespace CityInfo.Application.Features.Results.PointOfInterest
{
    public record CreatePointOfInterestResult(
        bool CityNotFound, 
        PointOfInterestDto? Created
    );
}
