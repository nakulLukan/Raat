using System.Threading.Tasks;

namespace Raat.Web.Contracts
{
    public interface IHttpContext
    {
        Task<T> ProcessPostRequest<T>(string apiEndpoint, object data);
        Task<T> ProcessGetRequest<T>(string apiEndpoint);
    }
}
