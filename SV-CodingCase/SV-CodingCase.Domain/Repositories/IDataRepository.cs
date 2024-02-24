using SV_CodingCase.Domain.Models;

namespace SV_CodingCase.Domain.Repositories
{
    public interface IDataRepository
    {
        public Task<DataFile> GetData();
    }
}
