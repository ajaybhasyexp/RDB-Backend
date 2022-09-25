using DataCollectorFunctions.Abstractions;
using DataCollectorFunctions.DataTransferObjects;
using DataCollectorFunctions.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DataCollectorFunctions.Functions
{
    public class DailyMovieDataUpdater
    {
        private readonly IRDBDataCollectorConfiguration _configuration;
        private readonly IAzureStorageWriter _azureStorageWriter;

        public DailyMovieDataUpdater(IRDBDataCollectorConfiguration configuration, IAzureStorageWriter azureStorageWriter)
        {
            _configuration = configuration;
            _azureStorageWriter = azureStorageWriter;
        }
        [FunctionName("DailyMovieDataUpdater")]
        public async Task Run(
            [QueueTrigger(Constants.FileDownloadQueue, Connection = "FileDownloadQueueStorage")] string myQueueItem,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var fileDownloadMessage = JsonConvert.DeserializeObject<FileDownloadMessage>(myQueueItem);
            var result = await _azureStorageWriter.ReadStorageBlobAsync(fileDownloadMessage.Containername, fileDownloadMessage.BlobName);
            var contents = result.Content.ToString();
            using (StringReader reader = new StringReader(contents))
            {
                string line = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        Console.WriteLine(line);
                    }

                } while (line != null);
            }
        }
    }
}
