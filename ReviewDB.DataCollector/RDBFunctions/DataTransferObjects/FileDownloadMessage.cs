namespace DataCollectorFunctions.DataTransferObjects
{
    public class FileDownloadMessage
    {
        public FileDownloadMessage(string blobName, FileDataType fileDataType, string containername)
        {
            BlobName = blobName;
            FileDataType = fileDataType;
            Containername = containername;
        }
        public string BlobName { get; set; }
        public string Containername { get; set; }
        public FileDataType FileDataType { get; set; }
    }
}
