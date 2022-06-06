namespace Formula1.Models
{
    public class ScheduleModel
    { 
        public int Round { get; set; }
        public string RaceName { get; set; }
        public CircuitModel Circuit { get; set; }
        public string Date { get; set; }
    }
}
