using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.Business.HttpService
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetDataAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7133/swagger/api/Users/register");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
