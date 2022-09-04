using Application.Contracts;
using Application.DTO;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using NexerAssignment.Response;

namespace NexerAssignment.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class MeasurementsController : ControllerBase
    {
        private readonly IDataService _dataService;
        
        public MeasurementsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("{deviceId}/{date}/{sensorType}")]
        public async Task<ActionResult<GetDataResponse>> GetData(string deviceId, DateTime date, SensorType sensorType, int resultCount)
        {
            //Fazer chamada para a camada de aplicação
            var data = await _dataService.GetDataFromService(new GetDataDto
            {
                DeviceId = deviceId,
                Date = date,
                Sensor = sensorType
            });

            if (resultCount <= data.Count)
            {
                data = data.Take(resultCount).ToList();
            }

            var dataResponse = new GetDataResponse
            {
                Measurements = data
            };


            return Ok(dataResponse);
        }

        [HttpGet("{deviceId}/{date}")]
        public async Task<ActionResult<GetDataResponse>> GetAllSensorsData(string deviceId, DateTime date, int resultCount)
        {
            //Fazer chamada para a camada de aplicação
            var data = await _dataService.GetDataFromService(new GetDataDto 
            {
                DeviceId = deviceId,
                Date = date
            });

            if (resultCount <= data.Count)
            {
                data = data.Take(resultCount).ToList();
            }

            var dataResponse = new GetDataResponse
            {
                Measurements = data
            };


            return Ok(dataResponse);
        }
    }
}