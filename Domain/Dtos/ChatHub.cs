using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Domain.Dtos
{
  public class ChatHub:Hub
    {
        public async Task sendToAll(string user,string message)
        {
            await Clients.All.SendAsync("sendToAll",user, message);
        }
                
        
    }
}