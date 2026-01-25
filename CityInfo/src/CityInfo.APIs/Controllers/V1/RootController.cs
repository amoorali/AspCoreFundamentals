using CityInfo.Application.DTOs.Link;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.APIs.Controllers.V1
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        #region [ GET Methods ]
        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot()
        {
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1";

            var links = new List<LinkDto>
            {
                new(Url.Link("GetRoot", new { version }),
                "self",
                "GET"),

                new(Url.Link("GetCitiesAsync", new { version }),
                "cities",
                "GET"),

                new(Url.Link("CreateCityAsync", new { version }),
                "create_city",
                "POST"),
            };

            return Ok(links);
        }
        #endregion
    }
}
