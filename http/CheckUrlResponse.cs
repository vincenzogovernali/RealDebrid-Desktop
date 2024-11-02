using Newtonsoft.Json;
using System.Text.Json;

namespace RealDebrid.model
{
    public class CheckUrlResponse
    {
        [JsonProperty("host")]
        private string host { get; set; }

        [JsonProperty("host_icon")]
        private string hostIcon { get; set; }

        [JsonProperty("host_icon_big")]
        private string hostIconBig { get; set; }

        [JsonProperty("link")]
        private string link { get; set; }

        [JsonProperty("filename")]
        private string filename { get; set; }

        [JsonProperty("filesize")]
        private long filesize { get; set; }

        [JsonProperty("supported")]
        private long supported { get; set; }

        public bool isSupported()
        {
            return supported.Equals(1L);
        }

        public string getFilename()
        {
            return this.filename;
        }


        public string getLink()
        {
            return this.link;
        }
    }
}
