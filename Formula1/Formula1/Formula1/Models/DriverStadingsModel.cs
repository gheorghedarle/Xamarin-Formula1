﻿using System.Collections.Generic;

namespace Formula1.Models
{
    public class DriverStadingsModel
    {
        public int Position { get; set; }
        public double Points { get; set; }
        public DriverModel Driver { get; set; }
        public List<TeamModel> Constructors { get; set; }
    }
}
