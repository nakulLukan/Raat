namespace Raat.Web.Services
{
    public static class Store
    {
        private static string myDisplayId = string.Empty;
        public static bool IsBusy { get; set; }
        public static string MyDisplayId { get => myDisplayId; }

        public static void UpdateDisplayId(string displayId)
        {
            myDisplayId = displayId;
        }
    }
}
