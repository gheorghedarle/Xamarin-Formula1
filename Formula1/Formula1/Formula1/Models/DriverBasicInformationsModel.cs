using System;
using System.Globalization;

namespace Formula1.Models
{
    public record DriverBasicInformationsModel
    {
        public string Team { get; set; }
        public string Country { get; set; }
        public string Podiums { get; set; }
        public string Points { get; set; }
        public string GrandsPrixEntered { get; set; }
        public string WorldChampionships { get; set; }
        public string HighestRaceFinish { get; set; }
        public string HighestGridPosition { get; set; }
        public string DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                DateTime dobDT = DateTime.ParseExact(DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(dobDT.ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                return age;
            }
        }
        public string PlaceOfBirth { get; set; }
    }
}
