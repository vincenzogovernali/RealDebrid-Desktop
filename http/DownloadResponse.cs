using Newtonsoft.Json;

namespace RealDebrid.model
{
    public class DownloadResponse
    {
        [JsonProperty("filename")]
        private string filename { get; set; }

        [JsonProperty("mimeType")]
        private string mimeType { get; set; }

        [JsonProperty("download")]
        private string download { get; set; }

        [JsonProperty("streamable")]
        private long streamable { get; set; }

        [JsonProperty("quality")]
        private string quality { get; set; }

        [JsonProperty("alternative")]
        private AlternativeModel[] alternative;

        public bool isStreamable()
        {
            return streamable.Equals(1L);
        }


        public string getFilename()
        {
            return filename;
        }

        public string getDownload()
        {
            return download;
        }

        public string getQuality()
        {
            return quality;
        }

        public AlternativeModel[] getAlternative()
        {
            return alternative;
        }


        public class AlternativeModel
        {
            [JsonProperty("download")]
            private string download { get; set; }

            [JsonProperty("quality")]
            private string quality { get; set; }


            public string getDownload()
            {
                return download;
            }

            public string getQuality()
            {
                return quality;
            }

        }
    }
}
