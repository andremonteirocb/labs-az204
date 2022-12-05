using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Collections.Generic;
using Flurl.Http;

namespace az204function.durable
{
    public static class Searching
    {
        [FunctionName("Searching")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            [DurableClient] IDurableOrchestrationClient starter)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string termo = req.Query["termo"];
            var instanceId = Guid.NewGuid().ToString();

            string output = await starter.StartNewAsync("Pesquisador", instanceId, termo);

            return starter.CreateCheckStatusResponse(req, instanceId);
        }

        [FunctionName("Pesquisador")]
        public static async Task<IEnumerable<string>> Search([OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var termo = context.GetInput<string>();

            var google = await context.CallActivityAsync<string>("Google", termo);
            var bing = await context.CallActivityAsync<string>("Bing", termo);
            var yahoo = await context.CallActivityAsync<string>("Yahoo", termo);

            return new string[] { google, bing, yahoo };
        }

        [FunctionName("Google")]
        public static async Task<string> SearchGoogle([ActivityTrigger] string termo)
        {
            return await $"https://google.com/search?=g={termo}"
            .GetAsync()
            .Result.GetStringAsync();
        }

        [FunctionName("Bing")]
        public static async Task<string> SearchBing([ActivityTrigger] string termo)
        {
            return await $"https://bing.com/search?=g={termo}"
            .GetAsync()
            .Result.GetStringAsync();
        }

        [FunctionName("Yahoo")]
        public static async Task<string> SearchYahoo([ActivityTrigger] string termo)
        {
            return await $"https://search.com/search?=p={termo}"
            .GetAsync()
            .Result.GetStringAsync();
        }
    }
}
