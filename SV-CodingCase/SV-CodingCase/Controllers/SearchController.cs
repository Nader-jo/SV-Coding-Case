using MediatR;
using Microsoft.AspNetCore.Mvc;
using SV_CodingCase.Domain.Application;

namespace SV_CodingCase.Controllers
{
    [ApiController]
    public class SearchController : BaseApiController
    {
        public SearchController(ILogger<SearchController> logger) : base(logger) { }

        [HttpGet]
        public async Task<IActionResult> Get([FromServices] IMediator mediator, string searchInput)
        {
            _logger.LogInformation("Searching for: " + searchInput);
            return HandleResult(await mediator.Send(new SearchRequest.Request(searchInput)));
        }
    }
}
