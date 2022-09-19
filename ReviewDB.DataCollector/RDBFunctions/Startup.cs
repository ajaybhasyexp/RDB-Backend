using DataCollectorFunctions.Abstractions;
using DataCollectorFunctions.DataTransferObjects;
using DataCollectorFunctions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(DataCollectorFunctions.Startup))]

namespace DataCollectorFunctions
{

    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string fileBaseUrl = Environment.GetEnvironmentVariable("APIFileBaseUrl");
            string storageConnectionString = Environment.GetEnvironmentVariable("AzureFileStorage");
            string apiBaseUrl = Environment.GetEnvironmentVariable("APIBaseUrl");
            string fileStorageConnectionString = Environment.GetEnvironmentVariable("FileDownloadStorage");

            var rdbConfig = new RDBDataCollectorConfiguration
            {
                APIFileBaseUrl = fileBaseUrl,
                AzureFileStorage = storageConnectionString,
                APIBaseUrl = apiBaseUrl,
                FileDownloadStorage = fileStorageConnectionString
            };
            builder.Services.AddSingleton<IRDBDataCollectorConfiguration>(rdbConfig);
            builder.Services.AddSingleton<IAzureStorageWriter, AzureStorageService>();
        }
    }
}
