using System.Collections.Generic;
using System.Linq;

namespace Formula1.Models
{
    public record ConstructorStadingsModel
    {
        public int Position { get; set; }
        public double Points { get; set; }
        public int Wins { get; set; }
        public ConstructorModel Constructor { get; set; }
        public List<DriverModel> Drivers { get; set; }
        public string DriversName { 
            get 
            {
                return string.Join(" | ", Drivers.Select(d => string.Format("{0}. {1}", d.GivenName.Substring(0, 1), d.FamilyName)));    
            } 
        }
    }
}
