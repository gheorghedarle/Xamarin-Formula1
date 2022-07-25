using System;

namespace Formula1.Models
{
    public class CircuitBasicInformationsModel
    {
        public string FirstGrandPrix { get; set; }
        public string NumberOfLaps { get; set; }
        public string CircuitLength { get; set; }
        public string RaceDistance { get; set; }
        public string LapRecord { get; set; }
        public double Length { get
            {
                return Convert.ToDouble(CircuitLength.Replace("km", "").Trim());
            } 
        }

        public double Distance
        {
            get
            {
                return Convert.ToDouble(RaceDistance.Replace("km", "").Trim());
            }
        }

        public string LapRecordTime
        {
            get
            {
                return LapRecord.TrimEnd().Replace("\n", "").Split(" ")[0];
            }
        }

        public string LapRecordDriver
        {
            get
            {
                return LapRecord.TrimEnd().Replace("\n", "").Split(" ", 2)[1];
            }
        }
    }
}
