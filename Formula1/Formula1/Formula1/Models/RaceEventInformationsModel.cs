using System.Collections.Generic;

namespace Formula1.Models
{
    public record RaceEventInformationsModel
    {
        public List<RaceEventModel> Races { get; set; }
    }
}
