using Microsoft.AspNetCore.SignalR.Client;
using RestSharp;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ChatLibrari;
using RestSharp.Serialization.Json;

namespace ClientNetCore

{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //RegisterUser();
            //Console.WriteLine(RegisterUser());
            var input = args[0];
            //Console.WriteLine(input);
            //string login;
            //string password;
            string token;
            //Console.Write("Enter your FirstName: ");
            //string  FirstName = Console.ReadLine();
            //Console.Write("Enter your LastName: ");
            //string LastName = Console.ReadLine();
            //Console.Write("Enter your Gender: ");
            //string Gender = Console.ReadLine();
            //Console.Write("Enter your Email: ");
            //string Email = Console.ReadLine();
            //Console.Write("Enter your Password: ");
            //string Password = Console.ReadLine();
            switch (input)
            {
                case "admin":
                    token = GetTokenAdmin();
                    break;

                case "qwerty":
                    token = GetTokenQwerty();
                    break;

                case "admin2":
                    token = RegisterUser();
                    break;

                default: throw new ArgumentException();
            }

            while (true)
            {
                Test(token);
                Console.ReadLine();
            }
        }

        public static string RegisterUser()
        {
            var client = new RestClient("http://localhost:50338/Account/Register");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "8fdd4a66-45fd-4308-80c1-896607c3c4b1");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\"FirstName\":\"dima\",\n\"LastName\":\"soniev\",\n\"Gender\":\"m\",\n\"Email\":\"admin2@gmail.com\",\n\"Password\":\"Qwerty12345&\"\n}", ParameterType.RequestBody);
            //request.AddParameter("undefined", $"{{\"FirstName\":{FirstName},\n\"LastName\":{LastName},\n\"Gender\":{Gender},\n\"Email\":{Email},\n\"Password\":{Password}\n}}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string error = JsonConvert.ToString(response.Content);

            return error;
        }

        public static string GetTokenQwerty()
        {
            Console.WriteLine(".Net Core 2.1");

            var client = new RestClient("http://localhost:50338/auth/gettoken");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "0811728e-9196-4f3e-a300-c7d399bea18b");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Basic YWRtaW5AZ21haWwuY29tOlF3ZXJ0eTEyMzQ1Jg==");
            IRestResponse response = client.Execute(request);

            string token = JsonConvert.ToString(response.Content);

            return token;
            //var anonynObj = new
            //{
            //    accessToken = string.Empty,
            //    username = string.Empty
            //};
            //var val = JsonConvert.DeserializeAnonymousType(response.Content, anonynObj);
            //return val.accessToken;
            //Data data = JsonConvert.DeserializeObject<Data>(response.Content);
            //string token = data.access_token;
            //string username = data.username;

            //return token;
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

            string token = JsonConvert.ToString(response.Content);

            return token;

            //var client = new RestClient("http://localhost:50338/token");
            //var request = new RestRequest(Method.POST);

            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            //request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"userName\"\r\n\r\nadmin@gmail.com\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"password\"\r\n\r\n12345\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    //var data = new JsonSerializer().Deserialize<Data>(response.Content);
            //    Data data = JsonConvert.DeserializeObject<Data>(response.Content);

            //    string token = data.access_token;
            //    string username = data.username;

            //Console.WriteLine($"{token},{username}");

            //}
            //var anonynObj = new
            //{
            //    acessToken =string.Empty,
            //     username = string.Empty
            //};

            //var data = JsonConvert.DeserializeAnonymousType(response.Content,anonynObj);
            //Data data = JsonConvert.DeserializeObject<Data>(response.Content);
            //string token = data.access_token;
            //string username = data.username;

            //return token;
        }

        public static void Test(string token)
        {
            try
            {
                Console.WriteLine(token);

                var connection = new HubConnectionBuilder()
                    .WithUrl("http://localhost:50338/chat",
                        options => { options.AccessTokenProvider = () => Task.FromResult(token); })
                    .Build();

                //connection.On<string, string>("ReceiveMessage", (name, message) => Console.WriteLine($"{name}  {message}"));
                connection.On<string, string>("ReceiveMessage", (sender, resJob) => Console.WriteLine($"sender {sender}\t mgs {resJob}"));
                //connection.On<string>("ReceiveMessageAuth", (resJob) => Console.WriteLine($"result Auth {resJob}"));
                //connection.On<string,string>("ReceiveMessageTask", Print);

                connection.StartAsync().Wait();

                Console.Write("Enter name interlocutor: ");
                var nameInterlocutor = Console.ReadLine();
                //Console.Write("Enter your name: ");
                //var password = Console.ReadLine();
                //Console.Write("Enter your surname: ");
                //var Fam = Console.ReadLine();

                //Console.Write("Enter your Login: ");
                //var Login = Console.ReadLine();
                //Console.Write("Enter your Password: ");
                //var Password = Console.ReadLine();
                //Console.Write("Enter your gender: ");
                //var Pol = Console.ReadLine();

                //connection.InvokeAsync("RecieveMessage", "HELLO").Wait();
                //connection.InvokeAsync("RecieveMessageAuth", "HelloMan").Wait();
                Console.WriteLine("input msg...");
                while (true)

                {
                    var message = Console.ReadLine();

                    connection.InvokeAsync("SendMessage", nameInterlocutor, message).Wait();
                    //connection.InvokeAsync("SendMessageAuth", virtualClientId, message).Wait();

                    //connection.InvokeAsync("RecieveMessageAuth", "HelloMan").Wait();
                    //connection.InvokeAsync("RecieveMessageAuth", "HelloMan").Wait();
                    //connection.InvokeAsync("RecieveMessage", message);
                    //connection.InvokeAsync("RecieveMessageAuth", message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void Print(string userName, string message)
        {
            Console.WriteLine($"{userName}  {message}  ");
        }
    }
}