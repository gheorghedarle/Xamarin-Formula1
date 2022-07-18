namespace Formula1.Models
{
    public record QualifyingResultModel
    {
        public int Number { get; set; }
        public int Position { get; set; }
        public DriverModel Driver { get; set; }
        public ConstructorModel Constructor { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
    }
}
