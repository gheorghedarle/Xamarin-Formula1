using Formula1.Models;
using System.Threading.Tasks;

namespace Formula1.Services.Informations
{
    public interface IInformationsService
    {
        Task<DriverInformationsModel> GetDriverInformations(string driver);
        Task<ConstructorBasicInformationsModel> GetTeamInformations(string team);
        Task<CircuitInformationsModel> GetCircuitInformations(string country);
    }
}
