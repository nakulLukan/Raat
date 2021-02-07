namespace Raat.Shared
{
    public static class SharedConstant
    {
        public const string ClientHubUrl = "/hubs/login";
        public const string ApiBaseUrl = "http://localhost:5001";
    }

    public static class RequestHeader
    {
        public const string DisplayId = "display-id";
    }

    public static class HubUrl
    {
        public const string RecieveDisplayId = "recieve-display-id";
        public const string RecieveConnectionRequest = "recieve-connection-request";
    }

    public static class HttpMessage
    {
        public const string UnknownClientUser = "UnknownClientUser";
    }

    public static class ApiEndpoint
    {
        public const string BeginChat = "/api/begin-chat";
        public const string RandomUser = "/api/random-user";
    }
}
