using EntityFrameworkLibrary.Models;


namespace EntityFrameworkLibrary.Repositories
{
    public interface IToDoListRepository
    {
        Task<string> AddItem(ToDoItem item);

        Task<ToDoItem> GetItemById(int id);
        
        Task<List<ToDoItem>> GetAllItems();

        Task<(string, ToDoItem)> UpdateItem(ToDoItem item);

        Task<string> DeleteItem(int id);
    }
}
