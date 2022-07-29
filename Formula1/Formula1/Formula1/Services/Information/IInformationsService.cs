using Formula1.Models;
using System.Threading.Tasks;

namespace Formula1.Services.Information
{
    public interface IInformationService
    {
        Task<DriverBasicInformationsModel> GetDriverInformation(string driver);
        Task<ConstructorBasicInformationsModel> GetTeamInformation(string team);
        Task<CircuitBasicInformationsModel> GetCircuitInformation(string country);
    }
}
