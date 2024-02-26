using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using SV_CodingCase.Domain.Models;
using SV_CodingCase.Domain.Models.Configuration;
using SV_CodingCase.Domain.Repositories;
using System.Net;
using System.Text.Json;

namespace SV_CodingCase.Test;
public class DataRepositoryTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<IMemoryCache> _memoryCacheMock;
    private readonly Mock<IOptions<ApplicationOptions>> _optionsMock;
    private readonly DataRepository _dataRepository;

    public DataRepositoryTests()
    {
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _memoryCacheMock = new Mock<IMemoryCache>();
        _optionsMock = new Mock<IOptions<ApplicationOptions>>();

        var options = new ApplicationOptions { DataSource = "http://test.com", CachingExpirationTimeInSeconds = 300 };
        _optionsMock.Setup(ap => ap.Value).Returns(options);

        var httpClient = new HttpClient(new TestHttpMessageHandler());
        _httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient);

        _dataRepository = new DataRepository(httpClient, _memoryCacheMock.Object, _optionsMock.Object);
    }

    [Fact]
    public async Task GetData_ReturnsDataFile()
    {
        var cacheEntry = Mock.Of<ICacheEntry>();
        object? expectedDataFile = new DataFile();
        _memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(cacheEntry);
        _memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out expectedDataFile)).Returns(false);

        var result = await _dataRepository.GetData();

        Assert.NotNull(result);
        Assert.IsType<DataFile>(result);
    }

    [Fact]
    public async Task GetData_CachesDataFile()
    {
        var cacheEntry = Mock.Of<ICacheEntry>();
        object? expectedDataFile = new DataFile();
        _memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(cacheEntry);
        _memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<object>(), out expectedDataFile)).Returns(false);

        var result = await _dataRepository.GetData();
        Assert.NotNull(result);
        _memoryCacheMock.Verify(x => x.CreateEntry(It.IsAny<object>()), Times.Once);
    }


    [Fact]
    public async Task GetData_UsesCacheIfAvailable()
    {
        object? expectedDataFile = new DataFile();
        _memoryCacheMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out expectedDataFile)).Returns(true);

        var result = await _dataRepository.GetData();

        _httpClientFactoryMock.VerifyNoOtherCalls();
    }

    private class TestHttpMessageHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(new DataFile()))
            };
            return await Task.FromResult(responseMessage);
        }
    }
}
