using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SV_CodingCase.Controllers;
using SV_CodingCase.Domain.Application;
using SV_CodingCase.Domain.Contract;
using SV_CodingCase.Domain.Models.Common;

namespace SV_CodingCase.Tests
{
    public class SearchControllerTests
    {
        private readonly Mock<ILogger<SearchController>> _loggerMock = new Mock<ILogger<SearchController>>();
        private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task Get_ReturnsOk_WhenDataFound()
        {
            var controller = new SearchController(_loggerMock.Object);
            var searchInput = "test";
            _mediatorMock.Setup(m => m.Send(It.IsAny<SearchRequest.Request>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<SearchResultDto>.Success(new SearchResultDto()));

            var result = await controller.Get(_mediatorMock.Object, searchInput);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenDataNotFound()
        {
            var controller = new SearchController(_loggerMock.Object);
            var searchInput = "notfound";
            _mediatorMock.Setup(m => m.Send(It.IsAny<SearchRequest.Request>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<SearchResultDto>.Failure("Data not found"));

            var result = await controller.Get(_mediatorMock.Object, searchInput);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Get_CallsMediator_WithCorrectSearchInput()
        {
            var controller = new SearchController(_loggerMock.Object);
            var searchInput = "test";
            string? mediatorReceivedInput = null;

            _mediatorMock.Setup(m => m.Send(It.IsAny<SearchRequest.Request>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<SearchResultDto>.Success(new SearchResultDto()))
                         .Callback<IRequest<Result<SearchResultDto>>, CancellationToken>(
                (request, ct) => mediatorReceivedInput = (request as SearchRequest.Request)?.SearchInput);

            await controller.Get(_mediatorMock.Object, searchInput);

            _mediatorMock.Verify(m => m.Send(It.IsAny<SearchRequest.Request>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(searchInput, mediatorReceivedInput);
        }

        [Fact]
        public async Task Get_LogsInformation_WithSearchInput()
        {
            var controller = new SearchController(_loggerMock.Object);
            var searchInput = "logtest";

            _mediatorMock.Setup(m => m.Send(It.IsAny<SearchRequest.Request>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<SearchResultDto>.Success(new SearchResultDto()));

            await controller.Get(_mediatorMock.Object, searchInput);

            _loggerMock.Verify(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(searchInput)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>())
            , Times.Once);
        }
    }
}
