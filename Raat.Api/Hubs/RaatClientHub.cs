using Microsoft.AspNetCore.SignalR;
using Raat.Api.Services;
using Raat.Shared;
using System;
using System.Threading.Tasks;

namespace Raat.Api.Hubs
{
    public class RaatClientHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            if (ClientService.InvertedClientConnections.ContainsKey(connectionId))
            {
                Context.Abort();
                Console.WriteLine($"Connection '{connectionId}' ABORTED...");
            }
            else
            {
                string displayConnectionId = Guid.NewGuid().ToString();
                ClientService.ClientConnections.Add(displayConnectionId, connectionId);
                ClientService.InvertedClientConnections.Add(displayConnectionId, connectionId);
                Console.WriteLine($"User '{displayConnectionId}' connected successfully!!!");
                Clients.Client(connectionId).SendAsync(HubUrl.RecieveDisplayId, displayConnectionId);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            if (ClientService.InvertedClientConnections.ContainsKey(connectionId))
            {
                var displayConnectionId = ClientService.InvertedClientConnections[connectionId];
                ClientService.InvertedClientConnections.Remove(connectionId);
                ClientService.ClientConnections.Remove(displayConnectionId);
                Console.WriteLine($"Connection '{connectionId}' ABORTED...");
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
