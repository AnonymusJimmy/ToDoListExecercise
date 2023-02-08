using EntityFrameworkLibrary.Models;
using EntityFrameworkLibrary.UnitOfWorks;
using Library.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoListFunc.Services
{
    public class ToDoService : IToDoService
    {
        public IUnitOfWork _unitOfWork { get; set; }
     
        public ToDoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ADD ITEM METHOD //
        public async Task<string> AddItem(ToDoItem item)
        {
            try
            {
                var existingItem = await _unitOfWork.ItemRepository.GetItemById(item.Id);
                if (existingItem != null)
                {
                    throw new AlreadyExistException();
                }
                await _unitOfWork.ItemRepository.AddItem(item);
                await _unitOfWork.SaveChangesAsync();
                return "Perfect, item has been successfully created";
            }
            catch (AlreadyExistException exc)
            {
                return exc.Message;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        //

        //UPDATE ITEM METHOD //
        public async Task<(string, ToDoItem)> UpdateItem(ToDoItem item)
        {
            try
            {
                var itemToUpdate = await _unitOfWork.ItemRepository.GetItemById(item.Id);
                if (itemToUpdate == null)
                {
                    throw new NotExistException($"Attention, item with id {item.Id} doesn't exist.");
                }
                await _unitOfWork.ItemRepository.UpdateItem(itemToUpdate);
                itemToUpdate.TaskName = item.TaskName;
                itemToUpdate.IsCompleted = item.IsCompleted;
                await _unitOfWork.SaveChangesAsync();
                return ("Perfect, item has been successfully updated", itemToUpdate);
            }/*
            catch(NotExistException exc)
            {
                return (exc.Message, null);
            }*/
            catch(Exception ex)
            {
                return (ex.Message, null);
            }
        }
        //

        //DELETE ITEM METHOD//
        public async Task<string> DeleteItem(int id)
        {
            try
            {
                var itemToDelete = await _unitOfWork.ItemRepository.GetItemById(id);
                if(itemToDelete == null)
                {
                    throw new NotExistException($"Attention, item with id {id} doesn't exist.");
                }
                await _unitOfWork.ItemRepository.DeleteItem(id);
                await _unitOfWork.SaveChangesAsync();
                return "Perfect, item has been successfully deleted.";
            }
            /*catch (NotExistException exc)
            {
                return exc.Message;
            }*/
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        //

        //GET ITEM METHOD//
        /*public async Task<ToDoItem> GetItemById(int id)
        {
            try
            {
                var itemToFind = await _unitOfWork.ItemRepository.GetItemById(id);
                if(itemToFind == null)
                {
                    throw new NotExistException($"Item with id {id} not found.");
                }
                return itemToFind;
            }
            catch (NotExistException exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
            catch(Exception ex)
            {
                var exception = ex.Message;
                return null;
            }
        }*/

        public async Task<ToDoItem> GetItemById(int id)
        {
            try
            {
                var itemToFind = await _unitOfWork.ItemRepository.GetItemById(id);
                return itemToFind;
            }
            catch (NotExistException exc)
            {
                throw new NotExistException($"item with id {id} not found");
            }
        }
        //

        //GET ALL ITEMS METHOD//
        public async Task<List<ToDoItem>> GetAllItems()
        {
            try
            {
                var items = await _unitOfWork.ItemRepository.GetAllItems();
                return items;
            }
            catch (Exception)
            {
                throw new Exception("Empty Database");
            }
        }
    }
}
