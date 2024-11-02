using Newtonsoft.Json;

using System.Numerics;

using System.Text.Json;


namespace RealDebrid.model
{
    public class TorrentResponse : TorrentCreateResponse
    {

        [JsonProperty("filename")]
        private string filename { get; set; }

        [JsonProperty("bytes")]
        private BigInteger bytes { get; set; }

        [JsonProperty("progress")]
        private Double progress { get; set; }

        [JsonProperty("status")]
        private string status { get; set; }

        [JsonProperty("links")]
        private List<string> links { get; set; }

        [JsonProperty("files")]
        private List<TorrentFile> files { get; set; }

        [JsonProperty("speed")]
        private BigInteger speed { get; set; }

        [JsonProperty("seeders")]
        private BigInteger seeders { get; set; }


        public string getFilename()
        {
            return filename;
        }

        public string getStatus()
        {
            return status;
        }

        public Double getProgress()
        {
            return progress;
        }

        public List<string> getLinks()
        {
            return links;
        }


        public class TorrentFile
        {
            [JsonProperty("id")]
            private long id { get; set; }
        }

    }
}
