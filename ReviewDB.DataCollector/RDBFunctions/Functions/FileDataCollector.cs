using System;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using DataCollectorFunctions.Abstractions;
using DataCollectorFunctions.DataTransferObjects;
using DataCollectorFunctions.Extensions;
using DataCollectorFunctions.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DataCollectorFunctions.Functions
{
    public class FileDataCollector
    {
        private readonly IRDBDataCollectorConfiguration _configuration;
        private readonly IAzureStorageWriter _azureStorageWriter;
        public FileDataCollector(IRDBDataCollectorConfiguration configuration, IAzureStorageWriter azureStorageWriter)
        {
            _configuration = configuration;
            _azureStorageWriter = azureStorageWriter;
        }
        [FunctionName("FileDataCollector")]        
        public async Task Run([TimerTrigger("30 09 * * *"
            //,RunOnStartup = true
            )] TimerInfo myTimer,
            [Queue("filedownload", Connection = "FileDownloadStorage")]
            ICollector<FileDownloadMessage> fileMessages,
            ILogger log)
        {
            try
            {
                log.LogInformation($"FileDataCollector Timer trigger function executed at: {DateTime.Now}");
                log.LogInformation("Starting file download");

                foreach (var fileDataType in (FileDataType[])Enum.GetValues(typeof(FileDataType)))
                {
                    var downloadUrl = CreateTodaysFileName(fileDataType);
                    var httpClient = new HttpClient();
                    var stream = await httpClient.GetStreamAsync(downloadUrl);
                    var blobName = downloadUrl.Replace(_configuration.APIFileBaseUrl, String.Empty);
                    var containerName = fileDataType.ToString().ToLower();

                    // Remove the extenstion (.gz)
                    blobName = blobName.Remove(blobName.Length - 3, 3);
                    using GZipStream decompressionStream = new GZipStream(
                        stream,
                        CompressionMode.Decompress);
                    await _azureStorageWriter.WriteStreamToStorageBlobAsync(
                    _configuration.FileDownloadStorage, containerName, blobName, decompressionStream);
                    log.LogInformation($"Downloaded {blobName}");
                    fileMessages.Add(new FileDownloadMessage(blobName, fileDataType, containerName));

                }

            }
            catch (Exception ex)
            {
                log.LogError(ex, "Exception while downloading task");
            }

        }
        private string CreateTodaysFileName(FileDataType fileDataType)
        {
            var today = DateTime.UtcNow;
            switch (fileDataType)
            {
                case FileDataType.Movies:
                    return $"{_configuration.APIFileBaseUrl}movie_ids_{today.FilePrefix()}.json.gz";
                case FileDataType.TVSeries:
                    return $"{_configuration.APIFileBaseUrl}tv_series_ids_{today.FilePrefix()}.json.gz";
                case FileDataType.People:
                    return $"{_configuration.APIFileBaseUrl}person_ids_{today.FilePrefix()}.json.gz";
                default: return $"{_configuration.APIFileBaseUrl}movie_ids_{today.FilePrefix()}.json.gz";
            }
        }
    }
}
