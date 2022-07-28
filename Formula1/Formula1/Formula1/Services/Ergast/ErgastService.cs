using Formula1.Core;
using Formula1.Helpers;
using Formula1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/{year}/driverStandings.json?{queryParams}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var res = json["MRData"]["StandingsTable"]["StandingsLists"].First["DriverStandings"].ToString();
                    var r = JsonConvert.DeserializeObject<List<DriverStadingsModel>>(res);
                    if (year == "current")
                    {
                        r.ForEach(d =>
                        {
                            d.Driver.Image = new DriverImageModel()
                            {
                                Side = $"{Constants.ImageApiBaseUrl}drivers/{d.Driver.DriverId}.png",
                                Front = $"{Constants.ImageApiBaseUrl}drivers/{d.Driver.DriverId}_front.png",
                            };
                            d.Constructors[0].Color = Constants.TeamColors[d.Constructors[0].ConstructorId];
                        });
                    }
                    return r;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<DriverModel> GetDriverInformations(string driver)
        {
            try
            {
                var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/drivers/{driver}.json");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var res = json["MRData"]["DriverTable"].ToString();
                    var r = JsonConvert.DeserializeObject<DriverInformationsModel>(res);
                    var d = r.Drivers.First();
                    d.Image = new DriverImageModel()
                    {
                        Side = $"{Constants.ImageApiBaseUrl}drivers/{d.DriverId}.png",
                        Front = $"{Constants.ImageApiBaseUrl}drivers/{d.DriverId}_front.png",
                    };
                    return d;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<ConstructorStadingsModel>> GetTeamStadings(string year, string queryParams = null)
        {
            try
            {
                var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/{year}/constructorStandings.json?{queryParams}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var res = json["MRData"]["StandingsTable"]["StandingsLists"].First["ConstructorStandings"].ToString();
                    var r = JsonConvert.DeserializeObject<List<ConstructorStadingsModel>>(res);
                    if (year == "current")
                    {
                        r.ForEach(c =>
                        {
                            c.Constructor.Color = Constants.TeamColors[c.Constructor.ConstructorId];
                        });
                        foreach (var a in r)
                        {
                            var dResponse = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/current/constructors/{a.Constructor.ConstructorId}/drivers.json");
                            if (dResponse.IsSuccessStatusCode)
                            {
                                var dResult = await dResponse.Content.ReadAsStringAsync();
                                var dJson = JObject.Parse(dResult);
                                var dRes = dJson["MRData"]["DriverTable"]["Drivers"].ToString();
                                a.Constructor.Drivers = JsonConvert.DeserializeObject<List<DriverModel>>(dRes);
                            }
                        }
                    }
                    return r;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<ConstructorModel> GetTeamInformations(string team)
        {
            try
            {
                var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/constructors/{team}.json");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var res = json["MRData"]["ConstructorTable"].ToString();
                    var r = JsonConvert.DeserializeObject<ConstructorInformationsModel>(res);
                    var c = r.Constructors.First();
                    c.Color = Constants.TeamColors[c.ConstructorId];
                    var dResponse = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/current/constructors/{c.ConstructorId}/drivers.json");
                    if (dResponse.IsSuccessStatusCode)
                    {
                        var dResult = await dResponse.Content.ReadAsStringAsync();
                        var dJson = JObject.Parse(dResult);
                        var dRes = dJson["MRData"]["DriverTable"]["Drivers"].ToString();
                        c.Drivers = JsonConvert.DeserializeObject<List<DriverModel>>(dRes);
                    }
                    return c;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<ScheduleModel> GetSchedule(string year, string queryParams = null)
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<RaceEventModel> GetRaceEventInformations(string year, int round)
        {
            try
            {
                var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/{year}/{round}.json");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var res = json["MRData"]["RaceTable"].ToString();
                    var r = JsonConvert.DeserializeObject<RaceEventInformationsModel>(res);
                    var c = r.Races.First();
                    c.Circuit.Location.Flag = $"{Constants.ImageApiBaseUrl}countries/{c.Circuit.Location.Country}.png";
                    c.Circuit.Map = $"{Constants.ImageApiBaseUrl}circuits/{c.Circuit.CircuitId}.png";
                    return c;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<RaceEventModel>> GetResults(string year, string round, string raceType, string queryParams = null)
        {
            try
            {
                var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/{year}/{round}/{raceType}.json?{queryParams}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var res = json["MRData"]["RaceTable"]["Races"].ToString();
                    var r = JsonConvert.DeserializeObject<List<RaceEventModel>>(res);
                    switch (raceType)
                    {
                        case "results":
                            {
                                if (r.Count > 0 && r.First().Results.Count > 0)
                                {
                                    r.First().Results.ForEach(d =>
                                    {
                                        d.Driver.Image = new DriverImageModel()
                                        {
                                            Side = $"{Constants.ImageApiBaseUrl}drivers/{d.Driver.DriverId}.png",
                                            Front = $"{Constants.ImageApiBaseUrl}drivers/{d.Driver.DriverId}_front.png"
                                        };
                                    });
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
                                    r.First().QualifyingResults.ForEach(d =>
                                    {
                                        d.Driver.Image = new DriverImageModel()
                                        {
                                            Side = $"{Constants.ImageApiBaseUrl}drivers/{d.Driver.DriverId}.png",
                                            Front = $"{Constants.ImageApiBaseUrl}drivers/{d.Driver.DriverId}_front.png"
                                        };
                                    });
                                    return r;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        case "sprint":
                            {
                                if (r.Count > 0 && r.First().SprintResults.Count > 0)
                                {
                                    r.First().SprintResults.ForEach(d =>
                                    {
                                        d.Driver.Image = new DriverImageModel()
                                        {
                                            Side = $"{Constants.ImageApiBaseUrl}drivers/{d.Driver.DriverId}.png",
                                            Front = $"{Constants.ImageApiBaseUrl}drivers/{d.Driver.DriverId}_front.png"
                                        };
                                    });
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<RaceEventModel>> GetResultsByDriver(string year, string driver)
        {
            try
            {
                var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/current/drivers/{driver}/results.json");
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<RaceEventModel>> GetResultsByTeam(string year, string team)
        {
            try
            {
                var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/current/constructors/{team}/results.json");
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<RaceResultsLapByLapModel>> GetResultsLapByLap(string year, int round, string driver)
        {
            try
            {
                var response = await _httpClientFactory.GetHttpClient().GetAsync($"https://ergast.com/api/f1/{year}/{round}/drivers/{driver}/laps.json?limit=100");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var res = json["MRData"]["RaceTable"]["Races"].ToString();
                    var r = JsonConvert.DeserializeObject<List<RaceResultsLapByLapModel>>(res);
                    if (r.Count > 0)
                    {
                        r.First().DriverImage = new DriverImageModel()
                        {
                            Side = $"{Constants.ImageApiBaseUrl}drivers/{driver}.png",
                            Front = $"{Constants.ImageApiBaseUrl}drivers/{driver}_front.png"
                        };
                        return r;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
