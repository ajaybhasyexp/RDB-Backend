using Azure.Storage.Blobs.Models;
using System.IO;
using System.Threading.Tasks;

namespace DataCollectorFunctions.Services
{
    public interface IAzureStorageWriter
    {
        Task WriteStreamToStorageBlobAsync(string containerName, string blobName, Stream stream);

        Task<BlobDownloadResult> ReadStorageBlobAsync(string containerName, string blobName);
    }
}
