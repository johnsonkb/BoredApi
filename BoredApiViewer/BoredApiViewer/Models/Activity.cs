using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;

namespace BoredApiViewer.Models
{
    public class BoredActivity 
    {
        public string key { get; set; }
        public string activity { get; set; }
        public string type { get; set; }
        public int participants { get; set; }
        public decimal price { get; set; }
        public string link { get; set; }    
        public decimal accessibility { get; set; }

        public async Task Set()
        {
            await Set("");
        }

        public async Task Set(string queryString)
        {
            var url = $"http://www.boredapi.com/api/activity{((queryString ?? "").Length > 0 ? $"?{queryString}" : "")}";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JsonConvert.PopulateObject(apiResponse, this);
                }
            }
        }
    }
}
