using Microsoft.AspNetCore.Mvc;
using Raat.Api.Contracts;
using Raat.Api.Services;
using Raat.Shared;
using System;
using System.Linq;

namespace Raat.Api.Controllers
{
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IRequestContext _requestContext;
        public ChatController(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        [HttpGet(ApiEndpoint.RandomUser)]
        public IActionResult FindRandomUser()
        {
            string myDisplayId = _requestContext.GetDisplayId();
            var allUsers = ClientService.ClientConnections.Keys.ToList();
            allUsers.Remove(myDisplayId);
            allUsers.RemoveAll(x=> ClientService.BusyUsers.Contains(x));
            Random random = new Random();
            string randomIdleUserId = allUsers[random.Next(allUsers.Count)];
            return Ok(new ResponseDto<string>(randomIdleUserId));
        }
    }
}
