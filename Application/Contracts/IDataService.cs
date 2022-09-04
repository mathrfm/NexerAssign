using Application.DTO;
using Domain.Models;

namespace Application.Contracts
{
    public interface IDataService
    {
        Task<List<Measurement>> GetDataFromService(GetDataDto request);
    }
}
