using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;
using GruzD.Web.Hubs;


namespace GruzD.Web.Hubs
{
    public class DefaultHub : Hub
    {
        public async Task SendMessage(IClientNotificator clientNotificator)
        {
            await this.Clients.Client(this.Context.ConnectionId).SendAsync(JsonConvert.SerializeObject(clientNotificator.Data));
        }
    }
}
