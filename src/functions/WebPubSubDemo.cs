using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Demo.WebPubSub.Models;
using Microsoft.Azure.WebJobs.Extensions.WebPubSub;
using Microsoft.Azure.WebPubSub.Common;

namespace Demo.WebPubSub.Api
{
    public static class WebPubSubDemo
    {
          
        [FunctionName("GetWebPubSubConnection")]
        public static WebPubSubConnection GetWebPubSubConnection(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req,
            [WebPubSubConnection(Hub = "demoHub", UserId = "{query.userid}", Connection  = "AzureWebPubSubConnectionString")] WebPubSubConnection connection)
        {
            Console.WriteLine("login");
            return connection;
        }
        

        [FunctionName("BroadcastProductIpdates")]
        public static async Task BroadcastProductIpdates([CosmosDBTrigger(
            databaseName: "demodb",
            containerName: "product",
            Connection  = "CosmosDBConnectionSetting",
            LeaseContainerName  = "leases", 
            FeedPollDelay = 500,
            CreateLeaseContainerIfNotExists  = true)] IReadOnlyList<Product> input,   
            [CosmosDB(
                databaseName: "demodb",
                containerName: "product",
                Connection = "CosmosDBConnectionSetting",
                SqlQuery = "SELECT * FROM c"
            )] IEnumerable<Product> products,   
           [WebPubSub(Hub = "demoHub", Connection  = "AzureWebPubSubConnectionString")] IAsyncCollector<WebPubSubAction> actions,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);

                await actions.AddAsync(
                new SendToAllAction
                {
                   // UserId = "democlient1",
                    Data = BinaryData.FromObjectAsJson(products),
                    DataType = WebPubSubDataType.Json
                });
            }
        }
    }
}