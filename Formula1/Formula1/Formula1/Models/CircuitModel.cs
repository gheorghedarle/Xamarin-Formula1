namespace Formula1.Models
{
    public record CircuitModel
    {
        public string CircuitId { get; set; }
        public string CircuitName { get; set; }
        public string RaceName { get; set; }
        public string Map { get; set; }
        public LocationModel Location { get; set; }
    }
}
