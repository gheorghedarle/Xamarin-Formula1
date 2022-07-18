using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    public record EventModel
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}
