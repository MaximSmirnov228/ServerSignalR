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
            Console.WriteLine("Hello World!");
            RegisterUser();
            string data = RegisterUser();
            Console.WriteLine(data);
            //while (true)
            //{
            //}
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
        }

        public static string RegisterUser()
        {
            var client = new RestClient("http://localhost:50338/Account/Register");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "8fdd4a66-45fd-4308-80c1-896607c3c4b1");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\"FirstName\":\"dima\",\n\"LastName\":\"soniev\",\n\"Gender\":\"m\",\n\"Email\":\"qwerty@gmail.com\",\n\"Password\":\"Qwerty12345&\"\n}", ParameterType.RequestBody);
            //request.AddParameter("undefined", $"{{\"FirstName\":{FirstName},\n\"LastName\":{LastName},\n\"Gender\":{Gender},\n\"Email\":{Email},\n\"Password\":{Password}\n}}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string error = JsonConvert.ToString(response.Content);

            return error;
        }
    }
}