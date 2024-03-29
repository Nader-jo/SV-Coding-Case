﻿using Microsoft.Extensions.Options;
using SV_CodingCase.Domain.Contract;
using SV_CodingCase.Domain.Models;
using SV_CodingCase.Domain.Models.Configuration;
using SV_CodingCase.Domain.Repositories;

namespace SV_CodingCase.Domain.Services
{
    public class SearchService : ISearchService
    {
        private readonly IDataRepository _dataRepository;
        private readonly ApplicationOptions _options;
        public SearchService(IDataRepository dataRepository, IOptions<ApplicationOptions> options)
        {
            _dataRepository = dataRepository;
            _options = options.Value;
        }

        public async Task<SearchResultDto> Search(string searchInput)
        {
            searchInput = searchInput.Trim();
            var rawData = await _dataRepository.GetData();
            var weights = InitializeDict(rawData);

            foreach (var item in rawData.Buildings)
            {
                if (MatchingStrings(item.Name, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Building.Name;
                    foreach (var lockItem in rawData.Locks)
                    {
                        if (lockItem.BuildingId == item.Id)
                        {
                            weights[lockItem.Id] += _options.WeightsConfiguration.Lock.BuildingId.Name;
                        }
                    }
                }
                if (MatchingStrings(item.ShortCut, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Building.ShortCut;
                    foreach (var lockItem in rawData.Locks)
                    {
                        if (lockItem.BuildingId == item.Id)
                        {
                            weights[lockItem.Id] += _options.WeightsConfiguration.Lock.BuildingId.ShortCut;
                        }
                    }
                }
                if (MatchingStrings(item.Description, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Building.Description;
                    foreach (var lockItem in rawData.Locks)
                    {
                        if (lockItem.BuildingId == item.Id)
                        {
                            weights[lockItem.Id] += _options.WeightsConfiguration.Lock.BuildingId.Description;
                        }
                    }
                }
            }
            foreach (var item in rawData.Groups)
            {
                if (MatchingStrings(item.Name, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Building.Name;
                    foreach (var lockItem in rawData.Locks)
                    {
                        if (lockItem.BuildingId == item.Id)
                        {
                            weights[lockItem.Id] += _options.WeightsConfiguration.Lock.BuildingId.Name;
                        }
                    }
                }
                if (MatchingStrings(item.Description, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Group.Description;
                    foreach (var lockItem in rawData.Media)
                    {
                        if (lockItem.GroupId == item.Id)
                        {
                            weights[lockItem.Id] += _options.WeightsConfiguration.Medium.GroupId.Description;
                        }
                    }
                }
            }
            foreach (var item in rawData.Locks)
            {
                if (MatchingStrings(item.Type, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Lock.Type;
                }
                if (MatchingStrings(item.Name, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Lock.Name;
                }
                if (MatchingStrings(item.SerialNumber, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Lock.SerialNumber;
                }
                if (MatchingStrings(item.Floor, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Lock.Floor;
                }
                if (MatchingStrings(item.RoomNumber, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Lock.RoomNumber;
                }
                if (MatchingStrings(item.Description, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Lock.Description;
                }
            }
            foreach (var item in rawData.Media)
            {
                if (MatchingStrings(item.Type, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Medium.Type;
                }
                if (MatchingStrings(item.Owner, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Medium.Owner;
                }
                if (MatchingStrings(item.SerialNumber, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Medium.SerialNumber;
                }
                if (MatchingStrings(item.Description, searchInput))
                {
                    weights[item.Id] += _options.WeightsConfiguration.Medium.Description;
                }
            }
            var result = new SearchResultDto()
            {
                Buildings = [],
                Locks = [],
                Groups = [],
                Medium = []
            };

            foreach (var item in rawData.Buildings)
            {
                if (weights[item.Id] > 0) result.Buildings.Add(new BuidingSearchResultDto() { Weight = weights[item.Id], Building = item });
            }
            foreach (var item in rawData.Locks)
            {
                if (weights[item.Id] > 0) result.Locks.Add(new LockSearchResultDto() { Weight = weights[item.Id], Lock = item });
            }
            foreach (var item in rawData.Groups)
            {
                if (weights[item.Id] > 0) result.Groups.Add(new GroupSearchResultDto() { Weight = weights[item.Id], Group = item });
            }
            foreach (var item in rawData.Media)
            {
                if (weights[item.Id] > 0) result.Medium.Add(new MediumSearchResultDto() { Weight = weights[item.Id], Medium = item });
            }

            return result;
        }


        private static Dictionary<Guid, int> InitializeDict(DataFile data)
        {
            var result = new Dictionary<Guid, int>();

            foreach (var item in data.Buildings)
            {
                result.Add(item.Id, 0);
            }
            foreach (var item in data.Locks)
            {
                result.Add(item.Id, 0);
            }
            foreach (var item in data.Groups)
            {
                result.Add(item.Id, 0);
            }
            foreach (var item in data.Media)
            {
                result.Add(item.Id, 0);
            }
            return result;
        }
        private static bool MatchingStrings(object? str1, string str2)
        {
            if (str1 is not null)
            {
                return str1.ToString()!.Contains(str2, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
