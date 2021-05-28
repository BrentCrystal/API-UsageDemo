using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DemoLibrary
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient
        }
    }

}
