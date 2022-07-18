using System.Collections.ObjectModel;

namespace Formula1.Models
{
    public record RaceEventResultsModel
    {
        public ObservableCollection<RaceResultModel> RaceResults { get; set; }
        public ObservableCollection<QualifyingResultModel> QualifyingResults { get; set; }
        public ObservableCollection<RaceResultModel> SprintResults { get; set; }
    }
}
