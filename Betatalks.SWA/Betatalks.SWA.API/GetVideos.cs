using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
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
            [Table("Videos")] CloudTable cloudTable, string partitionKey, ILogger log)
        {
            var videos = new List<VideoEntity>();
            TableQuerySegment<VideoEntity> querySegment = null;
            var query = new TableQuery<VideoEntity>().Where(TableQuery.GenerateFilterCondition(nameof(ITableEntity.PartitionKey), QueryComparisons.Equal, partitionKey));

            do
            {
                querySegment = await cloudTable.ExecuteQuerySegmentedAsync(query, querySegment?.ContinuationToken);
                videos.AddRange(querySegment.Results);
            } while (querySegment.ContinuationToken != null);

            return new OkObjectResult(videos);
        }
    }
}
