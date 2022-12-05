using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Flurl.Http;

namespace az204function.durable
{
    public static class Searching
    {
        [FunctionName("Searching")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var termo = req.Query["termo"];
            var instanceId = Guid.NewGuid().ToString();

            var output = await starter.StartNewAsync("Pesquisador", instanceId, termo);
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

        //[FunctionName("AddFromCounter")]
        //public static Task Run(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        //    [DurableClient] IDurableEntityClient client)
        //{
        //    var contador = req.Query["contador"];
        //    var entityId = new EntityId(nameof(Counter), "myCounter");
        //    var amount = int.Parse(contador);
        //    return client.SignalEntityAsync(entityId, "Add", amount);
        //}

        //[FunctionName("CounterOrchestration")]
        //public static async Task Run(
        //    [OrchestrationTrigger] IDurableOrchestrationContext context)
        //{
        //    var entityId = new EntityId(nameof(Counter), "myCounter");
        //    var currentValue = await context.CallEntityAsync<int>(entityId, "Get");
        //    if (currentValue < 5)
        //        context.SignalEntity(entityId, "Add", 1);
        //}

        //[FunctionName("Counter")]
        //public static void Counter([EntityTrigger] IDurableEntityContext ctx)
        //{
        //    switch (ctx.OperationName.ToLowerInvariant())
        //    {
        //        case "add":
        //            var currentValue = ctx.GetState<int>();
        //            var amount = ctx.GetInput<int>();
        //            if (currentValue < 100 && currentValue + amount >= 100)
        //            {
        //                ctx.SignalEntity(new EntityId("MonitorEntity", ""), "milestone-reached", ctx.EntityKey);
        //            }

        //            ctx.SetState(currentValue + amount);
        //            break;
        //        case "reset":
        //            ctx.SetState(0);
        //            break;
        //        case "get":
        //            ctx.Return(ctx.GetState<int>());
        //            break;
        //    }
        //}
    }
}
