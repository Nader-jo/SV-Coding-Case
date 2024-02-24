using MediatR;
using Microsoft.AspNetCore.Mvc;
using SV_CodingCase.Domain.Models.Common;

namespace SV_CodingCase.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseApiController : ControllerBase
    {

        public readonly ILogger<BaseApiController> _logger;
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public BaseApiController(ILogger<BaseApiController> logger) => _logger = logger;

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null)
                return NotFound(ResultDto.Fail("Entity not found"));
            if (result.IsSuccess && result.Value != null)
                return Ok(ResultDto<T>.Ok(result.Value));
            if (result.IsSuccess && result.Value == null)
                return NotFound(ResultDto.Fail("Entity not found"));
            return BadRequest(ResultDto.Fail(result.Error));
        }
    }
}