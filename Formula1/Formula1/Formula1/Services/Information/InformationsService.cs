using Formula1.Core;
using Formula1.Helpers;
using Formula1.Helpers.Extensions;
using Formula1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Formula1.Services.Information
{
    public class InformationService : IInformationService
    {
        private readonly HttpClientFactory _httpClientFactory;

        public InformationService(HttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<DriverBasicInformationsModel> GetDriverInformation(string driver)
        {
            try
            {
                var response = await _httpClientFactory.GetHttpClient().GetAsync($"{Constants.InformationsApiBaseUrl}driver/info?driver={driver.RemoveDiacritics()}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var r = json["result"].ToObject<DriverBasicInformationsModel>();
                    return r;
                }
                return null;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<ConstructorBasicInformationsModel> GetTeamInformation(string team)
        {
            try 
            { 
                var response = await _httpClientFactory.GetHttpClient().GetAsync($"{Constants.InformationsApiBaseUrl}team/info?team={team}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var r = json["result"].ToObject<ConstructorBasicInformationsModel>();
                    return r;
                }
                return null;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<CircuitBasicInformationsModel> GetCircuitInformation(string country)
        {
            try
            {
                var response = await _httpClientFactory.GetHttpClient().GetAsync($"{Constants.InformationsApiBaseUrl}circuit/info?country={country}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var r = json["result"].ToObject<CircuitBasicInformationsModel>();
                    return r;
                }
                return null;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
