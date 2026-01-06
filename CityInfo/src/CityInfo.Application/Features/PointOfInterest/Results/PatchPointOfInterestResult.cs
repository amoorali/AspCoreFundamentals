using CityInfo.Application.DTOs.PointOfInterest;

namespace CityInfo.Application.Features.PointOfInterest.Results
{
    #region [ Result Record ]
    public record PatchPointOfInterestResult(
        bool CityNotFound,
        bool PointOfInterestNotFound,
        PointOfInterestForUpdateDto? DtoToValidate,
        Dictionary<string, string>? PatchErrors
    );
    #endregion
}
