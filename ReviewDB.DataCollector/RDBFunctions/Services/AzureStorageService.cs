using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace DataCollectorFunctions.Services
{
    public class AzureStorageService : IAzureStorageWriter
    {
        public async Task WriteStreamToStorageBlobAsync(string connectionString, string containerName, string blobName, Stream stream)
        {
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            containerClient.CreateIfNotExists();
            BlobClient blobClient = GetBlobClient(connectionString, containerName, blobName);
            await blobClient.UploadAsync(stream, true);
        }

        /// <summary>
        /// This is virtual so we can mock the API calls.
        /// </summary>
        public virtual BlobClient GetBlobClient(string connectionString, string containerName, string blobName) => new BlobClient(connectionString, containerName, blobName);

    }
}
