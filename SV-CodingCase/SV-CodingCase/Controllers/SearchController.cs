using Microsoft.AspNetCore.Mvc;
using SV_CodingCase.Domain.Models.Common;

namespace SV_CodingCase.Controllers
{
    [ApiController]
    public class SearchController(ILogger<SearchController> logger) : BaseApiController(logger)
    {

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Get");
            return HandleResult<string>(Result<string>.Success(""));
        }
    }
}
