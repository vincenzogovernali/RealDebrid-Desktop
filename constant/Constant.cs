namespace RealDebrid.constant
{
    public class Constant
    {
        public static string BASE_URL { get; } = "https://api.real-debrid.com/rest/1.0";
        public static string CHECK_DOWNLOAD_URL { get; } = BASE_URL + "/unrestrict/check";
        public static string DOWNLOAD_URL { get; } = BASE_URL + "/unrestrict/link";
        public static string TORRENTS { get; } = BASE_URL + "/torrents";
        public static string TORRENTS_INFO { get; } = BASE_URL + "/torrents/info/";

        public static string TORRENTS_SELECT_FILES { get; } = BASE_URL + "/torrents/selectFiles/";

        public static string ADD_MAGNET { get; } = BASE_URL + "/torrents/addMagnet";
        public static string DELETE_TORRENT { get; } = BASE_URL + "/torrents/delete/";


        public static string BEARER { get; } = "Bearer";
        public static string AUTHORIZATION { get; } = "Authorization";
        public static string TOKEN { get; } = "token";
        public static string LINK { get; } = "link";
        public static string MAGNET { get; } = "magnet";
        public static string USER_AGENT { get; } = "User-Agent";
        public static string DEFAULT_USER_AGENT { get; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36";
        public static string FILES { get; } = "files";

    }
}
