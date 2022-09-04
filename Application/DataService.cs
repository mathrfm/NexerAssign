using Application.Contracts;
using Application.DTO;
using Domain.Models;
using Persistence;

namespace Application
{
    public class DataService : IDataService
    {
        private readonly IAzureRepository _azureRepository;

        public DataService(IAzureRepository azureRepository)
        {
            _azureRepository = azureRepository;
        }

        public async Task<List<Measurement>> GetDataFromService(GetDataDto request)
        {
            return await _azureRepository.GetCsvByDeviceIdDateAndType(request.DeviceId, request.Date, request.Sensor);
        }
    }
}
