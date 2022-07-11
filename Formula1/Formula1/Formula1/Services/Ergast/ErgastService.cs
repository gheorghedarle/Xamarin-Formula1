using Formula1.Core;
using Formula1.Helpers;
using Formula1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<DriverStadingsModel>> GetDriverStadings(string year, string queryParams = null)
        {
            var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/{year}/driverStandings.json?{queryParams}");
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

        public async Task<List<ConstructorStadingsModel>> GetTeamStadings(string year, string queryParams = null)
        {
            var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/{year}/constructorStandings.json?{queryParams}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                var res = json["MRData"]["StandingsTable"]["StandingsLists"].First["ConstructorStandings"].ToString();
                var r = JsonConvert.DeserializeObject<List<ConstructorStadingsModel>>(res);
                if(year == "current")
                {
                    foreach (var a in r)
                    {
                        var dResponse = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/current/constructors/{a.Constructor.ConstructorId}/drivers.json");
                        if (dResponse.IsSuccessStatusCode)
                        {
                            var dResult = await dResponse.Content.ReadAsStringAsync();
                            var dJson = JObject.Parse(dResult);
                            var dRes = dJson["MRData"]["DriverTable"]["Drivers"].ToString();
                            a.Drivers = JsonConvert.DeserializeObject<List<DriverModel>>(dRes);
                        }
                    }
                }
                return r;
            }
            return null;
        }

        public async Task<ScheduleModel> GetSchedule(string year, string queryParams = null)
        {
            var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/{year}.json?{queryParams}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                var res = json["MRData"]["RaceTable"]["Races"].ToString();
                var r = JsonConvert.DeserializeObject<List<RaceEventModel>>(res);
                r.ForEach(c => c.Circuit.Location.Flag = $"{Constants.ImageApiBaseUrl}countries/{c.Circuit.Location.Country}.png");
                r.ForEach(c => c.Circuit.Map = $"{Constants.ImageApiBaseUrl}circuits/{c.Circuit.CircuitId}.png");
                return new ScheduleModel()
                {
                    UpcomingRaceEvents = r.Where(a => a.Date > DateTime.Now).ToList(),
                    PastRaceEvents = r.Where(a => a.Date <= DateTime.Now).ToList()
                };
            }
            return null;
        }

        public async Task<List<RaceEventModel>> GetResults(string year, string round, string raceType, string queryParams = null)
        {
            var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/{year}/{round}/{raceType}.json?{queryParams}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                var res = json["MRData"]["RaceTable"]["Races"].ToString();
                var r = JsonConvert.DeserializeObject<List<RaceEventModel>>(res);
                switch(raceType)
                {
                    case "results":
                        {
                            if (r.Count > 0 && r.First().Results.Count > 0)
                            {
                                r.First().Results.ForEach(d => d.Driver.Image = $"{Constants.ImageApiBaseUrl}drivers/{d.Driver.Code}.png");
                                return r;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    case "qualifying":
                        {
                            if (r.Count > 0 && r.First().QualifyingResults.Count > 0)
                            {
                                r.First().QualifyingResults.ForEach(d => d.Driver.Image = $"{Constants.ImageApiBaseUrl}drivers/{d.Driver.Code}.png");
                                return r;
                            }
                            else
                            {
                                return null;
                            }
                        }
                }

            }
            return null;
        }

        public async Task<List<RaceEventModel>> GetResultsByDriver(string year, string driver)
        {
            var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/{year}/drivers/{driver}/results.json");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                var res = json["MRData"]["RaceTable"]["Races"].ToString();
                var r = JsonConvert.DeserializeObject<List<RaceEventModel>>(res);
                if (r.Count > 0)
                {
                    return r;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}
