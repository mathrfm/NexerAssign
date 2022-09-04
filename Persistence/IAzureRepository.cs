using Domain.Models;
using Domain.Models.Enums;

namespace Persistence
{
    public interface IAzureRepository
    {
        Task<List<Measurement>> GetCsvByDeviceIdDateAndType(string deviceId, DateTime date, SensorType? sensorType);
    }
}