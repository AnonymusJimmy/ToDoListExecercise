using EntityFrameworkLibrary.Models;
using EntityFrameworkLibrary.UnitOfWorks;
using Library.Exceptions.Exceptions;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListFunc.Dto;
using ToDoListFunc.Dtos;

namespace ToDoListFunc.Services
{
    public class ToDoService : IToDoService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private IMapper _mapper { get; set; }

        public ToDoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // ADD ITEM METHOD // 
        public async Task<string> AddItem(ToDoItemRequest item)
        {
            try
            {
                var itemToCreate = _mapper.Map<ToDoItem>(item);
                await _unitOfWork.ItemRepository.AddItem(itemToCreate);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Dispose();
                return "Perfect, item has been successfully created";
            }
            catch (CustomExeptions)
            {

                throw new CustomExeptions("Error during insert operation.");
            }
            
        }
        //


        //UPDATE ITEM METHOD //
        public async Task<(string, ToDoItemResponse)> UpdateItem(ToDoItemRequest itemReq, int id)
        {
            try
            {
                var itemToUpdate = await _unitOfWork.ItemRepository.GetItemById(id);
                if (itemToUpdate == null)
                {
                    throw new NotExistException($"Attention, item with id {id} doesn't exist.");
                }
                await _unitOfWork.ItemRepository.UpdateItem(itemToUpdate);
                itemToUpdate.TaskDescription = itemReq.TaskName;
                itemToUpdate.IsCompleted = itemReq.IsDone;

                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Dispose();
                var itemUpdatedDto = _mapper.Map<ToDoItemResponse>(itemToUpdate);
                return ("Perfect, item has been successfully updated", itemUpdatedDto);
            }/*
            catch(NotExistException exc)
            {
                return (exc.Message, null);
            }*/
            catch (Exception ex)
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
                if (itemToDelete == null)
                {
                    throw new NotExistException($"Attention, item with id {id} doesn't exist.");
                }
                await _unitOfWork.ItemRepository.DeleteItem(id);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.Dispose();
                return "Perfect, item has been successfully deleted.";
            }
            /*catch (NotExistException exc)
            {
                return exc.Message;
            }*/
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //


        //GET ITEM METHOD//
        public async Task<ToDoItemResponse> GetItemById(int id)
        {
            try
            {
                //Recupera il ToDoItem con l'id specificato da ToDoListRepository
                var itemToFind = await _unitOfWork.ItemRepository.GetItemById(id);
                if (itemToFind == null)
                    throw new NotExistException($"item with id {id} not found");

                //Mappa i dati recupareti dal repository ad un oggetto ToDoDto
                //var itemToFindDto = TypeAdapter.Adapt<ToDoItemDto>(itemToFind);
                var itemToFindDto = _mapper.Map<ToDoItemResponse>(itemToFind);
                _unitOfWork.Dispose();
                return itemToFindDto;
            }
            catch (NotExistException exc)
            {
                throw exc;
            }
        }
        //


        //GET ALL ITEMS METHOD//
        public async Task<List<ToDoItemResponse>> GetAllItems()
        {
            try
            {
                var items = await _unitOfWork.ItemRepository.GetAllItems();
                if (items == null)
                    throw new Exception();

                var itemsDto = _mapper.Map<List<ToDoItemResponse>>(items);
                _unitOfWork.Dispose();
                return itemsDto;
            }
            catch (Exception)
            {
                throw new Exception("Empty Database");
            }
        }
    }
}
