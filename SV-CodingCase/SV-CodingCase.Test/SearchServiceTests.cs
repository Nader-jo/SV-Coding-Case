using Microsoft.Extensions.Options;
using Moq;
using SV_CodingCase.Domain.Models;
using SV_CodingCase.Domain.Models.Configuration;
using SV_CodingCase.Domain.Repositories;
using SV_CodingCase.Domain.Services;

namespace SV_CodingCase.Test;
public class SearchServiceTests
{
    private readonly Mock<IDataRepository> _mockDataRepository;
    private readonly SearchService _searchService;
    private readonly ApplicationOptions _applicationOptions;

    public SearchServiceTests()
    {
        _mockDataRepository = new Mock<IDataRepository>();

        _applicationOptions = new ApplicationOptions
        {
            WeightsConfiguration = new WeightsConfiguration
            {
                Building = new Domain.Models.Configuration.Building { Name = 5, ShortCut = 3, Description = 1 },
                Lock = new Domain.Models.Configuration.Lock
                {
                    BuildingId = new Domain.Models.Configuration.Building { Name = 4, ShortCut = 2, Description = 1 },
                    Type = 3,
                    Name = 5,
                    SerialNumber = 4,
                    Floor = 2,
                    RoomNumber = 1,
                    Description = 2
                },
                Group = new Domain.Models.Configuration.Group { Name = 5, Description = 3 },
                Medium = new Medium { Type = 4, Owner = 5, SerialNumber = 3, Description = 2 }
            }
        };

        var optionsMock = new Mock<IOptions<ApplicationOptions>>();
        optionsMock.Setup(ap => ap.Value).Returns(_applicationOptions);

        _searchService = new SearchService(_mockDataRepository.Object, optionsMock.Object);
    }

    [Fact]
    public async Task Search_WithValidInput_ReturnsExpectedResults()
    {
        var testInput = "test";
        var dataFile = new DataFile
        {
            Buildings = [new Domain.Models.Building { Id = Guid.NewGuid(), Name = "Test Building" }],
            Locks = [],
            Groups = [],
            Media = []
        };

        _mockDataRepository.Setup(repo => repo.GetData()).ReturnsAsync(dataFile);

        var result = await _searchService.Search(testInput);

        Assert.NotEmpty(result.Buildings);
        _mockDataRepository.Verify(repo => repo.GetData(), Times.Once);
    }

    [Fact]
    public async Task Search_WithNoMatch_ReturnsEmptyResults()
    {
        var testInput = "no match";
        var dataFile = new DataFile
        {
            Buildings = [],
            Locks = [],
            Groups = [],
            Media = []
        };

        _mockDataRepository.Setup(repo => repo.GetData()).ReturnsAsync(dataFile);

        var result = await _searchService.Search(testInput);

        Assert.Empty(result.Buildings);
        Assert.Empty(result.Locks);
        Assert.Empty(result.Groups);
        Assert.Empty(result.Medium);
        _mockDataRepository.Verify(repo => repo.GetData(), Times.Once);
    }

    [Fact]
    public async Task Search_WithMatchingLocks_ReturnsLocksWithWeights()
    {
        var testInput = "lock";
        var lockId = Guid.NewGuid();
        var dataFile = new DataFile
        {
            Buildings = [],
            Locks = [new Domain.Models.Lock { Id = lockId, Name = "Test Lock" }],
            Groups = [],
            Media = []
        };

        _mockDataRepository.Setup(repo => repo.GetData()).ReturnsAsync(dataFile);

        var result = await _searchService.Search(testInput);

        Assert.NotEmpty(result.Locks);
        Assert.True(result.Locks[0].Weight > 0);
        _mockDataRepository.Verify(repo => repo.GetData(), Times.Once);
    }

    [Fact]
    public async Task Search_WithMatchingGroups_ReturnsGroupsWithWeights()
    {
        var testInput = "group";
        var groupId = Guid.NewGuid();
        var dataFile = new DataFile
        {
            Buildings = [],
            Locks = [],
            Groups = [new Domain.Models.Group { Id = groupId, Name = "Test Group" }],
            Media = []
        };

        _mockDataRepository.Setup(repo => repo.GetData()).ReturnsAsync(dataFile);

        var result = await _searchService.Search(testInput);

        Assert.NotEmpty(result.Groups);
        Assert.True(result.Groups[0].Weight > 0);
        _mockDataRepository.Verify(repo => repo.GetData(), Times.Once);
    }

    [Fact]
    public async Task Search_WithMatchingMedia_ReturnsMediaWithWeights()
    {
        var testInput = "Owner";
        var mediumId = Guid.NewGuid();
        var dataFile = new DataFile
        {
            Buildings = [],
            Locks = [],
            Groups = [],
            Media = [new Media { Id = mediumId, Owner = "Test Owner" }]
        };

        _mockDataRepository.Setup(repo => repo.GetData()).ReturnsAsync(dataFile);

        var result = await _searchService.Search(testInput);

        Assert.NotEmpty(result.Medium);
        Assert.True(result.Medium[0].Weight > 0);
        _mockDataRepository.Verify(repo => repo.GetData(), Times.Once);
    }
}
