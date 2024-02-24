using Microsoft.AspNetCore.Mvc;

namespace SV_CodingCase.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ControllerBaseApi : ControllerBase
    {
     
        private readonly ILogger<ControllerBaseApi> _logger;

        public ControllerBaseApi(ILogger<ControllerBaseApi> logger)
        {
            _logger = logger;
        }
    }
}