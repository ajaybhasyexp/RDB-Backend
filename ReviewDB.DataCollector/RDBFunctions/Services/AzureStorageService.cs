using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace DataCollectorFunctions.Services
{
    public class AzureStorageService : IAzureStorageWriter
    {
        private string _connectionString { get; set; }

        public AzureStorageService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task WriteStreamToStorageBlobAsync(string containerName, string blobName, Stream stream)
        {
            BlobContainerClient containerClient = new BlobContainerClient(_connectionString, containerName);
            containerClient.CreateIfNotExists();
            BlobClient blobClient = GetBlobClient(_connectionString, containerName, blobName);
            await blobClient.UploadAsync(stream, true);
        }

        public async Task<BlobDownloadResult> ReadStorageBlobAsync(string containerName, string blobName )
        {
            BlobClient blobClient = GetBlobClient(_connectionString, containerName, blobName);
            return await blobClient.DownloadContentAsync();

        }

        /// <summary>
        /// This is virtual so we can mock the API calls.
        /// </summary>
        public virtual BlobClient GetBlobClient(string connectionString, string containerName, string blobName) => new BlobClient(connectionString, containerName, blobName);

    }
}
