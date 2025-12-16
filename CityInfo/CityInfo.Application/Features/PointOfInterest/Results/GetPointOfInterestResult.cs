using CityInfo.Application.DTOs;

namespace CityInfo.Application.Features.PointOfInterest.Results
{
    public record GetPointOfInterestResult(
        bool CityNotFound,
        bool PointOfInterestNotFound,
        PointOfInterestDto? Item
    );
}
