using Microsoft.AspNetCore.Mvc;
using SV_CodingCase.Domain.Models;
using SV_CodingCase.Domain.Models.Common;
using SV_CodingCase.Domain.Repositories;

namespace SV_CodingCase.Controllers
{
    [ApiController]
    public class SearchController: BaseApiController
    {
        private readonly IDataRepository _dataRepository;

        public SearchController(ILogger<SearchController> logger, IDataRepository dataRepository): base(logger)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Get");
            return HandleResult<DataFile>(Result<DataFile>.Success(await _dataRepository.GetData()));
        }
    }
}
