using System.Collections.ObjectModel;

namespace Formula1.Models
{
    public class RaceEventResultsModel
    {
        public ObservableCollection<RaceResultModel> RaceResults { get; set; }
        public ObservableCollection<QualifyingResultModel> QualifyingResults { get; set; }
    }
}
