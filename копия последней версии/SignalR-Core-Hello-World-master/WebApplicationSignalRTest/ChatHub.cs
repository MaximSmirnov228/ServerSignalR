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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string idUser, string message)
        {
            var virtualClientId = Context.User.Identity.Name;
            await Clients.User(idUser).SendAsync("ReceiveMessage", virtualClientId, message + " " + DateTime.Now.ToShortTimeString());
            await Clients.Caller.SendAsync("ReceiveMessage", virtualClientId, message + " " + DateTime.Now.ToShortTimeString());
        }
    }
}