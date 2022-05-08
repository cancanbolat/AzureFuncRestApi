using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.Autofac;
using AzureFuncRestApi.Config;
using AzureFuncRestApi.Interfaces;

namespace AzureFuncRestApi.Functions
{
    //[DependencyInjectionConfig(typeof(DepInjectionConfig))]
    public class QueryFunctions
    {
        private readonly IProductService productService;

        public QueryFunctions(IProductService productService)
        {
            this.productService = productService;
        }

        [FunctionName("GetAll")]
        public async Task<IActionResult> GetAll(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products")] HttpRequest req,
            ILogger log)//, [Inject] IProductService productService)
        {
            log.LogInformation("GetAll function processed a request.");

            var products = await productService.GetAllAsync();

            return new OkObjectResult(products);
        }

        [FunctionName("GetById")]
        public async Task<IActionResult> GetById(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products/{id}")] HttpRequest req,
           ILogger log, int id)
        {
            log.LogInformation("Get function processed a request.");

            //var product = await productService.GetAsync(id);
            var product = $"id: {id}";

            return new OkObjectResult(product);
        }
    }
}
