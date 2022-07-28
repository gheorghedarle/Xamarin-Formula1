namespace Formula1.Models
{
    public record RaceResultModel
    {
        public int Number { get; set; }
        public int Position { get; set; }
        public double Points { get; set; }
        public int Grid { get; set; }
        public int Laps { get; set; }
        public string Status { get; set; }
        public DriverModel Driver { get; set; }
        public ConstructorModel Constructor { get; set; }
        public TimeModel Time { get; set; }
        public FastestLapModel FastestLap { get; set; }
    }
}
