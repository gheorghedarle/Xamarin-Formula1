namespace Formula1.Models
{
    public record ConstructorStadingsModel
    {
        public int Position { get; set; }
        public double Points { get; set; }
        public int Wins { get; set; }
        public ConstructorModel Constructor { get; set; }
    }
}
