using CityInfo.Application.DTOs.PointOfInterest;
using FluentValidation;

namespace CityInfo.Application.Validation.PointOfInterest
{
    public sealed class PointOfInterestForUpdateDtoValidator
        : PointOfInterestForManipulationDtoValidator<PointOfInterestForUpdateDto>
    {
        #region [ Constructor ]
        public PointOfInterestForUpdateDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("You should fill out a description.");
        }
        #endregion
    }
}
