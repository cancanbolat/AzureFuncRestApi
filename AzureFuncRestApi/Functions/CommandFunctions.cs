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
using AzureFuncRestApi.Models;

namespace AzureFuncRestApi.Functions
{
    //[DependencyInjectionConfig(typeof(DepInjectionConfig))]
    public class CommandFunctions
    {
        private readonly IProductService productService;

        public CommandFunctions(IProductService productService)
        {
            this.productService = productService;
        }

        [FunctionName("AddProduct")]
        public async Task<IActionResult> AddProduct(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequest req,
            ILogger log)//, [Inject] IProductService productService)
        {
            log.LogInformation("Create product function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<Product>(requestBody);

            var product = await productService.AddAsync(input);

            return new OkObjectResult(product);
        }

        [FunctionName("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "products/{id}")] HttpRequest req, ILogger log, int id)
        {
            log.LogInformation("Update product function processed a request");

            var product = await productService.GetAsync(id);

            if (product == null)
                return new NotFoundResult();

            
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<Product>(requestBody);

            await productService.UpdateAsync(id, input);
            

            return new OkObjectResult(input);
        }
    }
}
