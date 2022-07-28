using System.Collections.Generic;

namespace Formula1.Models
{
    public record LapModel
    {
        public int Number { get; set; }
        public List<TimingsModel> Timings { get; set; }
    }
}
