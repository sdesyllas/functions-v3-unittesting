using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionUnitTesting.Services;

namespace FunctionUnitTesting
{
    public class RecordFunction
    {
        private readonly IContext _dbContext;
        private readonly ITopicService _topicService;

        public RecordFunction(IContext dbContext, ITopicService topicService)
        {
            _dbContext = dbContext;
            _topicService = topicService;
        }

        [FunctionName("RecordFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "record/{id}")] HttpRequest req, string id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var record = new Domain.Record();
            return new OkObjectResult(record);
        }
    }
}
