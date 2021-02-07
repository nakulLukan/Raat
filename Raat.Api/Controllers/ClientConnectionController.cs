using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Raat.Api.Contracts;
using Raat.Api.Hubs;
using Raat.Api.Services;
using Raat.Shared;
using System.Threading.Tasks;

namespace Raat.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ClientConnectionController : ControllerBase
    {
        private readonly IRequestContext _requestContext;
        private readonly IHubContext<RaatClientHub> _hub;
        public ClientConnectionController(IRequestContext requestContext, IHubContext<RaatClientHub> hub)
        {
            _requestContext = requestContext;
            _hub = hub;
        }

        [HttpPost("begin-chat")]
        public async Task<IActionResult> BeginChat([FromBody]string userConnectionId)
        {
            string myDisplayId = _requestContext.GetDisplayId();
            if (ClientService.ClientConnections.ContainsKey(userConnectionId) && ClientService.ClientConnections.ContainsKey(myDisplayId))
            {
                ClientService.BusyUsers.Add(myDisplayId);
                ClientService.BusyUsers.Add(userConnectionId);
                await _hub.Clients.Client(ClientService.ClientConnections[userConnectionId]).SendAsync(HubUrl.RecieveConnectionRequest, myDisplayId);
                return Ok(new ResponseDto<bool>(true));
            }

            return NotFound(HttpMessage.UnknownClientUser);
        }
    }
}
