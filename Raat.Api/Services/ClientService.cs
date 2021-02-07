using System.Collections.Generic;

namespace Raat.Api.Services
{
    public static class ClientService
    {
        public static Dictionary<string, string> ClientConnections = new Dictionary<string, string>();
        public static Dictionary<string, string> InvertedClientConnections = new Dictionary<string, string>();
        public static HashSet<string> BusyUsers = new HashSet<string>();
    }
}
