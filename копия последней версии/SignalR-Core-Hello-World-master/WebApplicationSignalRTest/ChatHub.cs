using ChatLibrari;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplicationSignalRTest
{
    //public class ChatHub3:Hub
    //{
    //    public async Task Send(User user)
    //    {
    //        await this.Clients.All.SendAsync("ReceiveMessage",user);
    //    }


    //    public async Task RegisterData(User user)
    //    {
    //        await this.Clients.All.SendAsync("RegisterUser", user);
    //    }


        
    //}
   //[Authorize]
   // public class ChatHub45 : Hub
   // {
   //     public async Task Send(string user, string message)
   //     {
   //         //await this.Clients.Caller.SendAsync("ReceiveMessage",user, message);
   //         await this.Clients.Client(user).SendAsync("ReceiveMessage",user, message);
   //         //await this.Clients.Others.SendAsync("ReceiveMessage", user, message);
   //     }
   //     public async Task SendTest(string userName, string password)
   //     {
   //         //await this.Clients.Caller.SendAsync("ReceiveMessage",user, message);
   //         await this.Clients.Client(userName).SendAsync("ReceiveMessage", userName, password);
   //         //await this.Clients.Others.SendAsync("ReceiveMessage", user, message);
   //     }

   //     //[Authorize(Roles = "admin")]
   //     //public async Task Notify(string user, string message)
   //     //{
   //     //    await this.Clients.Caller.SendAsync("ReceiveMessage", user, message);
   //     //    await this.Clients.Client(user).SendAsync("ReceiveMessage", user, message);
   //     //    await this.Clients.Others.SendAsync("ReceiveMessage", user, message);
   //     //}




   // }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        

        public async Task SendMessage(string idUser,string message)
        {
            var virtualClientId = Context.User.Identity.Name;
            await Clients.User(idUser).SendAsync("ReceiveMessage", virtualClientId,message);
            await Clients.Caller.SendAsync("ReceiveMessage", virtualClientId,message);
        }

      
    }
}
