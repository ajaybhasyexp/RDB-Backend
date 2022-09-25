using DataCollectorFunctions.Abstractions;
using DataCollectorFunctions.DataTransferObjects;
using DataCollectorFunctions.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(DataCollectorFunctions.Startup))]

namespace DataCollectorFunctions
{

    class Startup : FunctionsStartup
    {
        public override async void Configure(IFunctionsHostBuilder builder)
        {
            string fileBaseUrl = Environment.GetEnvironmentVariable("APIFileBaseUrl");
            string apiBaseUrl = Environment.GetEnvironmentVariable("APIBaseUrl");
            string fileStorageConnectionString = Environment.GetEnvironmentVariable("FileDownloadQueueStorage");
            string dbEndpoint = Environment.GetEnvironmentVariable("DBEndpoint");
            string dbPrimaryKey = Environment.GetEnvironmentVariable("DBPrimaryKey");
            var cosmosClient = new CosmosClient(dbEndpoint, dbPrimaryKey,
                                                new CosmosClientOptions() { ApplicationName = Constants.CosmosDBAccountName });
            await cosmosClient.CreateDatabaseIfNotExistsAsync(Constants.CosmosDBName);

            var rdbConfig = new RDBDataCollectorConfiguration
            {
                APIFileBaseUrl = fileBaseUrl,
                APIBaseUrl = apiBaseUrl,
                FileDownloadQueueStorage = fileStorageConnectionString
            };
            builder.Services.AddSingleton<IRDBDataCollectorConfiguration>(rdbConfig);
            builder.Services.AddSingleton<IAzureStorageWriter>(new AzureStorageService(fileStorageConnectionString));
            builder.Services.AddSingleton<ICosmosDBService>(new CosmosDBService(cosmosClient));
        }
    }
}
