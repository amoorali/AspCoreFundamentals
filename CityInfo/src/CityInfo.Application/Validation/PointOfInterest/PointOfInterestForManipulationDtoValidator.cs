using CityInfo.Application.DTOs.PointOfInterest;
using FluentValidation;

namespace CityInfo.Application.Validation.PointOfInterest
{
    public abstract class PointOfInterestForManipulationDtoValidator<T>
        : AbstractValidator<T> where T : PointOfInterestForManipulationDto
    {
        #region [ Constructor ]
        protected PointOfInterestForManipulationDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("You should provide a name value")
                .MaximumLength(100).WithMessage("The name shouldn't have more than 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(1500).WithMessage("The description shouldn't have more than 1500 characters");
        }
        #endregion
    }
}
