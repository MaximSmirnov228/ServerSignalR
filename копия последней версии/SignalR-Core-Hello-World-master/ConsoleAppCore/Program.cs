using Microsoft.AspNetCore.SignalR.Client;
using RestSharp;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ChatLibrari;

namespace ClientNetCore

{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var input = args[0];
            //Console.WriteLine(input);
            //string login;
            //string password;
            string token;
            switch (input)
            {
                case "admin":
                     token = GetTokenAdmin();
                    break;
                case "qwerty":
                    token = GetTokenQwerty();
                    break;
                default: throw new ArgumentException();
            }

            while (true)
            {
                Test(token);
                Console.ReadLine();
            }

        }

        public static string GetTokenQwerty()
        {
            var client = new RestClient("http://localhost:50338/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "d34a96d9-ed00-4362-9c36-e22de088eed7");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"userName\"\r\n\r\nqwerty\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"password\"\r\n\r\n55555\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //var anonynObj = new
            //{
            //    accessToken = string.Empty,
            //    username = string.Empty
            //};
            //var val = JsonConvert.DeserializeAnonymousType(response.Content, anonynObj);
            //return val.accessToken;
            Data data = JsonConvert.DeserializeObject<Data>(response.Content);
            string token = data.access_token;
            string username = data.username;

            return token;
        }


        public static string GetTokenAdmin()
        {
            Console.WriteLine(".Net Core 2.1");

            var client = new RestClient("http://localhost:50338/token");
            var request = new RestRequest(Method.POST);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"userName\"\r\n\r\nadmin@gmail.com\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"password\"\r\n\r\n12345\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
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
            Data data = JsonConvert.DeserializeObject<Data>(response.Content);
            string token = data.access_token;
            string username = data.username;

            return token;
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
                connection.On<string, string>("ReceiveMessage", (sender,resJob) => Console.WriteLine($"sender {sender}\t mgs {resJob}"));
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
                    

                    connection.InvokeAsync("SendMessage", nameInterlocutor, message ).Wait();
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

        private static void Print(string userName,string message)
        {
            Console.WriteLine($"{userName}  {message}  ");
        }
    }
}