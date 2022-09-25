namespace DataCollectorFunctions.Abstractions
{
    public interface IRDBDataCollectorConfiguration
    {
        public string APIFileBaseUrl { get; set; }
        public string APIBaseUrl { get; set; }
        public string AzureFileStorage { get; set; }
        public string FileDownloadQueueStorage { get; set; }
    }
}
