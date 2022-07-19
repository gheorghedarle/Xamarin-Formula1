﻿using Formula1.Core;
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
                var r = json["result"].ToObject<DriverInformationsModel>();
                return r;
            }
            return null;
        }

        public async Task<ConstructorBasicInformationsModel> GetTeamInformations(string team)
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

        public async Task<CircuitInformationsModel> GetCircuitInformations(string country)
        {
            var response = await _httpClientFactory.GetHttpClient().GetAsync($"{Constants.InformationsApiBaseUrl}circuit/info?country={country}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                var r = json["result"].ToObject<CircuitInformationsModel>();
                return r;
            }
            return null;
        }
    }
}
