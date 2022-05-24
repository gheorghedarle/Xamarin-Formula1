using Formula1.Core;
using Formula1.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Formula1.Services.Ergast
{
    public class ErgastService : IErgastService
    {
        private readonly HttpClientFactory _httpClientFactory;

        public ErgastService(HttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<DriverModel>> GetDriverStadings()
        {
            var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/current/driverStandings.json");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                var res = json["MRData"]["StandingsTable"]["StandingsLists"].First["DriverStandings"];
                return new List<DriverModel>();
            }
            return null;
        }
    }
}
