using Microsoft.AspNetCore.Mvc;
using SV_CodingCase.Domain.Application;

namespace SV_CodingCase.Controllers
{
    [ApiController]
    public class SearchController : BaseApiController
    {
        public SearchController(ILogger<SearchController> logger) : base(logger){}

        [HttpGet]
        public async Task<IActionResult> Get(string searchInput)
        {
            _logger.LogInformation("Get");
            return HandleResult(await Mediator.Send(new SearchRequest.Request(searchInput)));
        }
    }
}
