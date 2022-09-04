using Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class GetDataDto
    {
        public string DeviceId { get; set; }

        public DateTime Date { get; set; }

        public SensorType Sensor { get; set; }
    }
}
