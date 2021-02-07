using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Raat.Shared;
using Raat.Web.Contracts;
using Raat.Web.JSServices;
using Raat.Web.Services;
using Raat.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Raat.Web.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public ClipboardService Clipboard { get; set; }

        [Inject]
        public IHttpContext HttpContext { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private string myConnectionId = "......";
        public string MyConnectionId {
            get => myConnectionId;
            set
            {
                myConnectionId = value;
                Store.UpdateDisplayId(MyConnectionId);
            }
        }
        public RandomConnection Connection { get; set; } = new RandomConnection();
        public bool IsConnected;
        private HubConnection connection;

        protected override async Task OnInitializedAsync()
        {
            string hubConnectionUrl = SharedConstant.ApiBaseUrl + SharedConstant.ClientHubUrl;
            connection = new HubConnectionBuilder()
                .WithUrl(hubConnectionUrl)
                .WithAutomaticReconnect()
                .Build();
            await connection.StartAsync();
            IsConnected = connection.State == HubConnectionState.Connected || connection.State == HubConnectionState.Connecting;
            StateHasChanged();
            connection.Remove(HubUrl.RecieveDisplayId);
            connection.On<string>(HubUrl.RecieveDisplayId, OnDisplayIdRecieved);
            connection.On<string>(HubUrl.RecieveConnectionRequest, OnConnectionRecieved);
            Console.WriteLine($"Connected: {IsConnected}, ConnectionId: {connection.ConnectionId}");
        }

        public async Task OnSubmit()
        {
            if (!Store.IsBusy)
            {
                Console.WriteLine($"Connecting to user '{Connection.Id}'");
                var begin = await HttpContext.ProcessPostRequest<ResponseDto<bool>>(ApiEndpoint.BeginChat, Connection.Id);
                    Store.IsBusy = true;
                    NavigationManager.NavigateTo("rooms/private/" + Connection.Id);
            }
            else
            {
                Console.WriteLine($"You are already in another chat session. Disconnect from there");
            }
        }

        public async Task FindRandomUser()
        {
            var randomUserId = await HttpContext.ProcessGetRequest<ResponseDto<string>>(ApiEndpoint.RandomUser);
            Connection.Id = randomUserId.Data;
            Console.WriteLine($"Random user id: {randomUserId.Data}");
            StateHasChanged();
        }

        private void OnDisplayIdRecieved(string displayConnectionId)
        {
            MyConnectionId = displayConnectionId;
            StateHasChanged();
        }

        private void OnConnectionRecieved(string connectionId)
        {
            Console.WriteLine($"Incomming connection: {connectionId}");
            if (!Store.IsBusy)
            {
                Connection.Id = connectionId;
                Store.IsBusy = true;
                StateHasChanged();
                NavigationManager.NavigateTo("rooms/private/" + connectionId);
            }
        }

        public async Task CopyMyIdToClipboard()
        {
            await Clipboard.WriteTextAsync(MyConnectionId);
            Console.WriteLine($"Copied to clipboard");
        }
    }
}
