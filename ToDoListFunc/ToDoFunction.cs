using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ToDoListFunc.Dto;
using ToDoListFunc.Dtos;
using ToDoListFunc.Services;


[assembly: FunctionsStartup(typeof(ToDoListFunc.Startup.MyStartup))]

namespace ToDoListFunc
{
    public class ToDoFunction
    {
        // (DEPENDENCY INJECTION) Inject service class into ours "controller".
        private readonly IToDoService _service;

        public ToDoFunction(IToDoService service)
        {
            _service = service;
        }

        /* ADD A NEW ITEM */
        [FunctionName("AddItem")]
        [OpenApiOperation(operationId:"AddItem", tags: new[] { "Add new Item" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ToDoItemRequest)/*, Description = "Parameters", Example = typeof(Parameter)*/)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Created, Description = "Item added")]
        public async Task<IActionResult> AddItem(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "item/add")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Adding new item.");
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var item = JsonConvert.DeserializeObject<ToDoItemRequest>(requestBody);
            await _service.AddItem(item);
            return new OkObjectResult(item);
        }


        /* GET ITEM BY ID*/
        [FunctionName("GetItemById")]
        [OpenApiOperation(operationId: "GetItemById", tags: new[] { "Get Item by Id" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Insert Value")]
        public async Task<IActionResult> GetItemById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "item/{id}")] HttpRequest req,
            int id, ILogger log)
        {
            log.LogInformation("Getting Item by Id.");
            var itemToFind = await _service.GetItemById(id);
            return new OkObjectResult(itemToFind);
        }


        /* GET All ITEMS */
        [FunctionName("GetAllItems")]
        [OpenApiOperation(operationId: "GetAllItems", tags: new[] { "Get all items" })]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "All Items")]
        public async Task<IActionResult> GetAllItems([HttpTrigger(AuthorizationLevel.Function, "get", Route = "allitems/")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting all items.");
            var items = await _service.GetAllItems();
            return new OkObjectResult(items);
        }


        /*UPDATE ITEM*/
        [FunctionName("UpdateItem")]
        [OpenApiOperation(operationId: "UpdateItem", tags: new[] { "Update Item" })]
        [OpenApiParameter(name: "id", Required = true, Type = typeof(int), Description = "Item id")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ToDoItemRequest))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType:"application/json", bodyType: typeof(ToDoItemResponse), Description = "Returns Updated Item.")]
        public async Task<IActionResult> UpdateItem([HttpTrigger(AuthorizationLevel.Function, "put", Route = "updateitem/{id}")] HttpRequest req, ILogger log, int id)
        {
            log.LogInformation("Updating item.");
            var requestBody = await new StreamReader (req.Body).ReadToEndAsync();
            var itemToUpdate = JsonConvert.DeserializeObject<ToDoItemRequest>(requestBody);
            var result = await _service.UpdateItem(itemToUpdate, id);
            return new OkObjectResult(result);
        }
        //


        /*DELETE ITEM*/
        [FunctionName("DeleteItem")]
        [OpenApiOperation(operationId: "DeleteItem", tags: new[] { "Remove Item" })]
        [OpenApiParameter(name: "id", Required = true, Type = typeof(int), Description ="Item Id")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "Ok Response")]
        public async Task<IActionResult> DeleteItem([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "deleteItem/{id}")] HttpRequest req,
            int id, ILogger log)
        {
            log.LogInformation("Deleting item,");
            var itemToDelete = await _service.DeleteItem(id);
            return new OkObjectResult(itemToDelete);
        }

    }
}
