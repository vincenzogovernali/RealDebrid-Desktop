using Newtonsoft.Json;
using System.Text.Json;

namespace RealDebrid.model
{
    public class TorrentCreateResponse
    {

        [JsonProperty("id")]
        private string id { get; set; }


        public string getId()
        {
            return id;
        }

    }
}
