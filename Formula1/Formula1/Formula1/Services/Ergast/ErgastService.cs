using Formula1.Core;
using Formula1.Helpers;
using Formula1.Models;
using Newtonsoft.Json;
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

        public async Task<List<DriverStadingsModel>> GetDriverStadings(string year)
        {
            var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/{year}/driverStandings.json");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                var res = json["MRData"]["StandingsTable"]["StandingsLists"].First["DriverStandings"].ToString();
                var r = JsonConvert.DeserializeObject<List<DriverStadingsModel>>(res);
                r.ForEach(d => d.Driver.Image = $"{Constants.ImageApiBaseUrl}drivers/{d.Driver.Code}.png");
                return r;
            }
            return null;
        }

        public async Task<List<ConstructorStadingsModel>> GetTeamStadings(string year)
        {
            var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/{year}/constructorStandings.json");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                var res = json["MRData"]["StandingsTable"]["StandingsLists"].First["ConstructorStandings"].ToString();
                var r = JsonConvert.DeserializeObject<List<ConstructorStadingsModel>>(res);
                return r;
            }
            return null;
        }
    }
}
