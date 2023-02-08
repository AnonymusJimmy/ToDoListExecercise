using EntityFrameworkLibrary.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoListFunc.Services
{
    // Interfaccia di Service in cui definiamo i metodi che andranno poi implementati nella classe concreta
    // Service.cs in cui sarà contenuta la logica di business.

    public interface IToDoService
    {
        Task<string> AddItem(ToDoItem item);
        Task<string> DeleteItem(int id);
        Task<(string, ToDoItem)> UpdateItem(ToDoItem item);
        Task<ToDoItem> GetItemById(int id);
        Task<List<ToDoItem>> GetAllItems();
    }
}
