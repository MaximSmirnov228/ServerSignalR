using System;
using Microsoft.AspNetCore.SignalR.Client;
using RestSharp;

using System;

using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ChatLibrari;
using Newtonsoft.Json.Serialization;

namespace TestAuth
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string data;
            Console.WriteLine("Hello World!");
            while (true)
            {
                data = GetTokenAdmin();
                Console.WriteLine(data);
            }
        }

        public static string GetTokenAdmin()
        {
            Console.WriteLine(".Net Core 2.1");

            var client = new RestClient("http://localhost:50338/auth/gettoken");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "0811728e-9196-4f3e-a300-c7d399bea18b");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Basic YWRtaW5AZ21haWwuY29tOlF3ZXJ0eTEyMzQ1Jg==");
            IRestResponse response = client.Execute(request);

            string data = JsonConvert.ToString(response.Content);
            //string token = data.access_token;
            //string username = data.username;
            return data;
        }
    }
}