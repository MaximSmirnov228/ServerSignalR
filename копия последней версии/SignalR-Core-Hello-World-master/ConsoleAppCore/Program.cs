using System;

namespace ClientNetCore

{
    internal class Program
    {
        private const string ServerAddress = "http://localhost:50338";
        private const string user1 = "1@mail.com";
        private const string user2 = "admin@gmail.com";

        private static void Main(string[] args)
        {
            #region User1

            var c = new Client(ServerAddress);
            try
            {
                c.Register(user1, "Qwerty12345&", "ivan", "Ivanov", Client.Gender.Men);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ;
            }

            c.ReceiveMessage = (sender, message) => Console.WriteLine(sender + " " + message);
            c.Login(user1, "Qwerty12345&");
            var users = c.GetAllUsers();

            Console.WriteLine("All users");
            foreach (var VARIABLE in users)
            {
                Console.WriteLine(VARIABLE);
            }
            c.StartChat();

            #endregion User1

            #region User2

            var c2 = new Client(ServerAddress);
            try
            {
                c2.Register(user2, "Qwerty12345&", "ivan", "Ivanov", Client.Gender.Men);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ;
            }

            c2.ReceiveMessage = (sender, message) => Console.WriteLine(sender + " " + message);
            c2.Login(user2, "Qwerty12345&");

            Console.WriteLine("All users");
            foreach (var VARIABLE in users)
            {
                Console.WriteLine(VARIABLE);
            }
            c2.StartChat();

            #endregion User2

            c.SendMessage(user2, "hi");
            c2.SendMessage(user1, "hi");

            Console.ReadLine();
        }
    }
}