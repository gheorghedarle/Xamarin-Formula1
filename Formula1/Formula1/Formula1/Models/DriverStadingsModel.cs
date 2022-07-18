using System.Collections.Generic;

namespace Formula1.Models
{
    public record DriverStadingsModel
    {
        public int Position { get; set; }
        public double Points { get; set; }
        public DriverModel Driver { get; set; }
        public List<ConstructorModel> Constructors { get; set; }
    }
}
