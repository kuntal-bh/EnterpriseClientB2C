using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseClientB2C.Services
{
    public class ReceipeClient : IReceipeClient
    {
        private readonly string resourceIdforReceipeAPI;

        private readonly string clientId;
        private readonly string clientSecret;
        private bool tokenset;
        private readonly string authority;
        private readonly HttpClient httpClient;

        public ReceipeClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            resourceIdforReceipeAPI = configuration["ReceipeAPI:ResourceId"];

            clientId = configuration["AzureAd:ClientId"];
            clientSecret = configuration["AzureAd:ClientSecret"];
            authority = $"{configuration["AzureAd:Instance"]}{configuration["AzureAd:TenantId"]}";
            this.httpClient.BaseAddress = new Uri(configuration["ReceipeAPI:baseUrl"]);
        }

        public async Task SetTokenforNews()
        {
            if (!tokenset)
            {
                var authContext = new AuthenticationContext(authority);
                var credentials = new ClientCredential(clientId, clientSecret);
                var authResult = await authContext.AcquireTokenAsync(resourceIdforReceipeAPI, credentials);
                var accessToken = authResult.AccessToken;
                this.httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "a8c2d6487c2e4441b36f67c2c0578748");
                this.httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                tokenset = true;
            }
        }

        public async Task<string[]> GetValuesforNews()
        {
            await SetTokenforNews();
            var res = await this.httpClient.GetAsync("/ReceipeAPI/api/values");
            // var res = await this.httpClient.GetAsync("/api/values");
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception($"{res.StatusCode}");
            }

            var content = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<string[]>(content);
        }
    }
}
