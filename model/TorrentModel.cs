namespace RealDebrid.model
{
    public class TorrentModel
    {
        public string id { get; set; }

        public string filename { get; set; }

        public Double progress { get; set; }

        public string status { get; set; }

        public List<string> links { get; set; }


        public TorrentModel(TorrentResponse response)
        {
            id = response.getId();
            filename = response.getFilename();
            progress = response.getProgress();
            links = response.getLinks();
            status = response.getStatus();
        }

    }
}
