using System.IO;
using System.Threading.Tasks;

namespace DataCollectorFunctions.Services
{
    public interface IAzureStorageWriter
    {
        Task WriteStreamToStorageBlobAsync(string connectionString, string containerName, string blobName, Stream stream);
    }
}
