using System.Threading.Tasks;

using Azure.Data.Tables;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Betatalks.SWA.API.Entities;

namespace Betatalks.SWA.API
{
    public static class GetVideos
    {
        [FunctionName(nameof(GetVideos))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "videos/{partitionKey}")] HttpRequest req,
            [Table("Videos", Connection = "StorageConnectionString")] TableClient tableClient, string partitionKey, ILogger log)
        {
            var videos = tableClient.Query<VideoEntity>();

            return new OkObjectResult(videos);
        }
    }
}
