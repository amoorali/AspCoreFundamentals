using Asp.Versioning;
using CityInfo.Application.DTOs.PointOfInterest;
using CityInfo.Application.Features.PointOfInterest.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.APIs.Controllers.V2
{
    [ApiController]
    [Authorize(Policy = "MustBeFromAntwerp")]
    [ApiVersion(2)]
    [Route("api/v{version:apiVersion}/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        #region [ Fields ]
        private readonly IMediator _mediator;
        #endregion

        #region [ Constructor ]
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMediator mediator)
        {
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
        }
        #endregion

        #region [ GET Methods ]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterestAsync(int cityId)
        {
            var cityName = User.Claims.First(c => c.Type == "city").Value;

            var result = await _mediator.Send(new GetPointsOfInterestQuery(cityId, cityName));

            if (result.Forbid)
                return Forbid();

            return Ok(result.Items);

        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterestAsync(int cityId, int pointOfInterestId)
        {
            var result = await _mediator.Send(new GetPointOfInterestQuery(cityId, pointOfInterestId));

            if (result.PointOfInterestNotFound)
                return NotFound("Point of interest not found");

            return Ok(result.Item);
        }
        #endregion
    }
}
