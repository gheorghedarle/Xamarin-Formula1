using System;

namespace Formula1.Models
{
    public record EventModel
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}
