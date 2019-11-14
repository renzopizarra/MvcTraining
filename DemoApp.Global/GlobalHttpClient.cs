using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Global
{
    public class GlobalHttpClient
    {
        public static HttpClient httpClient = new HttpClient();

        static GlobalHttpClient()
        {
            httpClient.BaseAddress = new Uri("https://localhost:44346/api/");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
    }
}
