namespace Formula1.Helpers
{
    public static class Constants
    {
        #if DEBUG
            public static string ImageApiBaseUrl = "http://10.0.2.2:4000/";
        #else
            public static string ImageApiBaseUrl = "Github Url";
        #endif
    }
}
