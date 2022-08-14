using DataCollectorFunctions.Abstractions;

namespace DataCollectorFunctions.DataTransferObjects
{
    public class RDBDataCollectorConfiguration : IRDBDataCollectorConfiguration
    {
        public string APIFileBaseUrl { get; set; }
        public string APIBaseUrl { get; set; }
        public string AzureFileStorage { get; set; }
    }
}
