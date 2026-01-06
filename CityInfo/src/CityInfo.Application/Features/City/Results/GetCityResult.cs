using CityInfo.Application.DTOs.City;
using System.Dynamic;

namespace CityInfo.Application.Features.City.Results
{
    public record GetCityResult(
        bool NotFound,
        ExpandoObject? Item
    );
}
