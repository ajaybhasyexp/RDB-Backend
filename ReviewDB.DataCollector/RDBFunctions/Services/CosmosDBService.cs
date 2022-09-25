using Microsoft.Azure.Cosmos;

namespace DataCollectorFunctions.Services
{
    public class CosmosDBService : ICosmosDBService
    {
        private CosmosClient _cosmosClient { get; set; }

        public CosmosDBService(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }
    }
}
