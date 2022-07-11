using Formula1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Formula1.Services.Ergast
{
    public interface IErgastService
    {
        Task<List<DriverStadingsModel>> GetDriverStadings(string year, string queryParams = null);
        Task<List<ConstructorStadingsModel>> GetTeamStadings(string year, string queryParams = null);
        Task<ScheduleModel> GetSchedule(string year, string queryParams = null);
        Task<List<RaceEventModel>> GetResults(string year, string round, string raceType, string queryParams = null);
        Task<List<RaceEventModel>> GetResultsByDriver(string year, string driver);
    }
}
