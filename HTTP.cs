using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TTS.PingKing
{
    class HTTP
    {
        private static string targetAddress;

        public HTTP(string address)
        {
            targetAddress = address;
        }

        public async Task SendHTTPAsync()
        {
            HttpClient http = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                response = await http.GetAsync(targetAddress);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Status Code " + (int)response.StatusCode + ": " + response.ReasonPhrase);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            http.Dispose();
        }
    }
}
