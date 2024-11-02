namespace RealDebrid.model
{
    public class DownloadModel
    {
        public string filename { get; set; }
        public string mimeType { get; set; }
        public string download { get; set; }
        public bool streamable { get; set; }


        public DownloadModel(DownloadResponse response)
        {
            this.filename = response.getFilename() + " " + (response.getQuality() != null ? response.getQuality() : "");
            this.download = response.getDownload();
            this.streamable = response.isStreamable();
        }


        public DownloadModel(DownloadResponse response, DownloadResponse.AlternativeModel model)
        {
            this.filename = response.getFilename() + " " + model.getQuality() != null ? model.getQuality() : "";
            this.download = model.getDownload();
            this.streamable = response.isStreamable();
        }

    }
}
