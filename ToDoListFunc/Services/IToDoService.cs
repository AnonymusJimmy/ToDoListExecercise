using EntityFrameworkLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListFunc.Dto;
using ToDoListFunc.Dtos;

namespace ToDoListFunc.Services
{
    // Interfaccia di Service in cui definiamo i metodi che andranno poi implementati nella classe concreta
    // Service.cs in cui sarà contenuta la logica di business.

    public interface IToDoService
    {
        Task<string> AddItem(ToDoItemRequest item);
        Task<string> DeleteItem(int id);
        Task<(string, ToDoItemResponse)> UpdateItem(ToDoItemRequest item, int id);
        Task<ToDoItemResponse> GetItemById(int id);
        Task<List<ToDoItemResponse>> GetAllItems();
    }
}
