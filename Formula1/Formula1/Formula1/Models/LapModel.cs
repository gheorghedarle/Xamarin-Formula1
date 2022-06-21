namespace Formula1.Models
{
    public class LapModel
    {
        public int Rank { get; set; }
        public int Lap { get; set; }
        public TimeModel Time { get; set; }
        public SpeedModel AverageSpeed { get; set; }
    }
}
