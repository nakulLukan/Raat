using Microsoft.AspNetCore.Http;
using Raat.Api.Contracts;
using Raat.Shared;
using System.Linq;

namespace Raat.Api.Services
{
    public class RequestContext : IRequestContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetDisplayId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var headers = httpContext.Request.Headers.ToDictionary(x => x.Key, x => x.Value);
            if (headers.ContainsKey(RequestHeader.DisplayId))
            {
                return headers[RequestHeader.DisplayId];
            }

            return null;
        }
    }
}
