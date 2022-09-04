namespace Domain.Models
{
    public class Measurement
    {
        public string DeviceId { get; set; }

        public string SensorType { get; set; }

        public DateTime Date { get; set; }

        public float Value { get; set; }
    }
}
