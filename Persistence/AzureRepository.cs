using Azure.Storage.Blobs;
using Domain.Models;
using Domain.Models.Enums;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public class AzureRepository: IAzureRepository
    {
        private IStorageFactory _myStorageFactory;
        private readonly string _sensors;

        public AzureRepository(IStorageFactory myStorageFactory, IConfiguration config)
        {
            _sensors = config.GetSection("DefaultSensors").Value;
            _myStorageFactory = myStorageFactory;
        }

        public async Task<List<Measurement>> GetCsvByDeviceIdDateAndType(string deviceId, DateTime date, SensorType? sensorType)
        {
            var sensors = _sensors.Split("|");
            var list = new List<Measurement>();
            var container = await _myStorageFactory.GetBlobContainer();
            
            if (sensorType != null)
                sensors = sensorType.ToString().Split("|");

            for(int i = 0; i < sensors.Length; i++)
            {
                string search = deviceId + "/" + sensors[i] + "/" + date.ToString("yyyy-MM-dd") + ".csv";

                BlobClient blobClient = container.GetBlobClient(search.ToLower());
                if (!await blobClient.ExistsAsync())
                    continue;

                var file = await blobClient.DownloadAsync();

                using (var streamReader = new StreamReader(file.Value.Content))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var line = await streamReader.ReadLineAsync();
                        var split = line.Split(";");
                        list.Add(new Measurement
                        {
                            DeviceId = deviceId,
                            SensorType = sensors[i],
                            Date = DateTime.Parse(split[0]),
                            Value = GetFloatValue(split[1])
                        });
                    }
                }
            }

            return list;
        }

        private float GetFloatValue(string value)
        {

            var multiplier = 1;

            if (value.StartsWith('-'))
            {
                multiplier = -1;
                value = value.Substring(1, value.Length-1);
            }

            if (value.StartsWith(','))
            {
                value = "0" + value;
            }

            return float.Parse(value) * multiplier;

        }
    }
}
