using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using EntityFrameworkLibrary.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using ToDoListFunc.Services;
using System.IO;
using Newtonsoft.Json;

[assembly: FunctionsStartup(typeof(ToDoListFunc.Startup.MyStartup))]  

namespace ToDoListFunc
{
    public class ToDoFunction
    {
        // (DEPENDENCY INJECTION) Inject service class into ours "controller".
        private readonly IToDoService _service;   
        public ToDoFunction(IToDoService service)
        {
            _service= service;
        }

        /* ADD A NEW ITEM */
        [FunctionName("AddItem")]
        public async Task<IActionResult> AddItem([HttpTrigger(AuthorizationLevel.Function, "post", Route = "todo/add")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Adding new item.");
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var newItem = JsonConvert.DeserializeObject<ToDoItem>(requestBody);
            var result = await _service.AddItem(newItem);
            return new OkObjectResult(result);
        }


        /* GET ITEM BY ID*/
        [FunctionName("GetItemById")]
        public async Task<IActionResult> GetItemById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo/{id}")] HttpRequest req,
            int id, ILogger log)
        {
            log.LogInformation("Getting Item by Id.");
            var itemToFind = await _service.GetItemById(id);
            return new OkObjectResult(itemToFind);
        }


        /* GET All ITEMS */
        [FunctionName("GetAllItems")]
        public async Task<IActionResult> GetAllItems([HttpTrigger(AuthorizationLevel.Function, "get", Route = "alltodo/")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting all items.");
            var items = await _service.GetAllItems();
            return new OkObjectResult(items);
        }

        /*UPDATE ITEM*/
        [FunctionName("UpdateItem")]
        public async Task<IActionResult> UpdateItem([HttpTrigger(AuthorizationLevel.Function, "put", Route = "todo/update")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Updating item.");
            var requestBody = await new StreamReader (req.Body).ReadToEndAsync();
            var itemToUpdate = JsonConvert.DeserializeObject<ToDoItem>(requestBody);
            var result = await _service.UpdateItem(itemToUpdate);
            return new OkObjectResult(result);
        }
        //


        /*DELETE ITEM*/
        [FunctionName("DeleteItem")]
        public async Task<IActionResult> DeleteItem([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "delete/{id}")] HttpRequest req,
            int id, ILogger log)
        {
            log.LogInformation("Deleting item,");
            var itemToDelete = await _service.DeleteItem(id);
            return new OkObjectResult(itemToDelete);
        }

    }
}
