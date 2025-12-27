namespace CityInfo.Application.DTOs.PointOfInterest
{
    public abstract class PointOfInterestForManipulationDto
    {
        #region [ Fields ]
        public string Name { get; set; } = string.Empty;

        public virtual string? Description { get; set; }
        #endregion
    }
}
