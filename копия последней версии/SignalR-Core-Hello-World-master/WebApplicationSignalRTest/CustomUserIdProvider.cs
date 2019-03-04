using Microsoft.AspNetCore.SignalR;

namespace WebApplicationSignalRTest
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.Identity.Name;
        }
    }
}