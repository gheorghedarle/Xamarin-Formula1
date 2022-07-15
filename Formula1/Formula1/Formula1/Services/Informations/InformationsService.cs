using Formula1.Core;
using Formula1.Helpers;
using Formula1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Formula1.Services.Informations
{
    public class InformationsService : IInformationsService
    {
        private readonly HttpClientFactory _httpClientFactory;

        public InformationsService(HttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<DriverInformationsModel> GetDriverInformations(string driver)
        {
            var response = await _httpClientFactory.GetHttpClient().GetAsync($"{Constants.InformationsApiBaseUrl}driver/info?driver={driver}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                var r = json.ToObject<DriverInformationsModel>();
                return r;
            }
            return null;
        }

        public async Task<ConstructorInformationsModel> GetTeamInformations(string team)
        {
            var response = await _httpClientFactory.GetHttpClient().GetAsync($"{Constants.InformationsApiBaseUrl}team/info?team={team}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                var r = json.ToObject<ConstructorInformationsModel>();
                return r;
            }
            return null;
        }
    }
}
