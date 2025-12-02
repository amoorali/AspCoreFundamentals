using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Interfaces;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepostory;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;

        public CitiesController(ICityInfoRepository cityInfoRepostory,
            IMapper mapper)
        {
            _cityInfoRepostory = cityInfoRepostory ??
                throw new ArgumentNullException(nameof(cityInfoRepostory));
            _mapper = mapper 
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCitiesAsync(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCitiesPageSize)
                pageSize = maxCitiesPageSize;

            var (cityEntities, paginationMetadata) = await _cityInfoRepostory
                .GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityAsync(
            int id, bool includePointsOfInterest = false)
        {
            var cityEntity = await _cityInfoRepostory.GetCityAsync(id, includePointsOfInterest);

            if (cityEntity == null)
                return NotFound("The id isn't in the collection");

            if (includePointsOfInterest)
                return Ok(_mapper.Map<CityDto>(cityEntity));

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(cityEntity));
        }
    }
}
