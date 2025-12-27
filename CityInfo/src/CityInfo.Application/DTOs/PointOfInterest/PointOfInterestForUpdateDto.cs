namespace CityInfo.Application.DTOs.PointOfInterest
{
    public class PointOfInterestForUpdateDto : PointOfInterestForManipulationDto
    {
        #region [ Fields ]
        public override string? Description { 
            get => base.Description; set => base.Description = value; }
        #endregion
    }
}
