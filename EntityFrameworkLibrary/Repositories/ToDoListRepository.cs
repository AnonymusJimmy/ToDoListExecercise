using EntityFrameworkLibrary.Context;
using EntityFrameworkLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkLibrary.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ToDoListDbContext _context;

        public ToDoListRepository(ToDoListDbContext context)
        {
            _context = context;
        }

       
        public async Task<string> AddItem(ToDoItem item)
        {
            await _context.AddAsync(item);
            //_context.SaveChangesAsync(); --> SPOSTATO NEL REPOSITORY
            return string.Empty;
        }
 
        //
        //

        public async Task<(string, ToDoItem)> UpdateItem(ToDoItem item)
        {
            ToDoItem? itemToUpdate = await GetItemById(item.Id);
            _context.ToDoItems.Update(itemToUpdate);
            //_context.SaveChangesAsync(); --> SPOSTATO NEL REPOSITORY
            return (string.Empty, item);
            
        }

        //
        //

        public async Task<string> DeleteItem(int id)
        {
            var itemToDelete = await GetItemById(id);
            _context.ToDoItems.Remove(itemToDelete);
            //_context.SaveChangesAsync(); --> SPOSTATE NEL REPOSITORY
            return string.Empty;
        }

        //
        //

        public async Task<List<ToDoItem>> GetAllItems()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        //
        //

        public async Task<ToDoItem?> GetItemById(int id)
        {
            return await _context.ToDoItems.FindAsync(id);
        }
    }
}
