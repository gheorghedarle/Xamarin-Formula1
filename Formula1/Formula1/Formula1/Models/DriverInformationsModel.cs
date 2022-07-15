using System;

namespace Formula1.Models
{
    public class DriverInformationsModel
    {
        public string Country { get; set; }
        public int Podiums { get; set; }
        public double Points { get; set; }
        public int GrandsPrixEntered { get; set; }
        public int WorldChampionships { get; set; }
        public string HighestRaceFinish { get; set; }
        public string HighestGridPosition { get; set; }
        public string DateOfBirth { get; set; }
        public int Age { get
            {
                var dobDT = DateTime.Parse(DateOfBirth);
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(dobDT.ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                return age;
            }
        }
        public string PlaceOfBirth { get; set; }
    }
}
