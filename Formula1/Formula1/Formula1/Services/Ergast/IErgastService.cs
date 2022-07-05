using Formula1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Formula1.Services.Ergast
{
    public interface IErgastService
    {
        Task<List<DriverStadingsModel>> GetDriverStadings(string year);
        Task<List<ConstructorStadingsModel>> GetTeamStadings(string year);
        Task<ScheduleModel> GetSchedule(string year);
        Task<List<RaceEventModel>> GetResults(string year, string round, string raceType);
        Task<List<RaceEventModel>> GetResultsByDriver(string year, string driver);
    }
}
