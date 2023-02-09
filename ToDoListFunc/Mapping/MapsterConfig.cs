using EntityFrameworkLibrary.Models;
using Mapster;
using ToDoListFunc.Dto;
using ToDoListFunc.Dtos;

namespace ToDoListFunc.Mapping
{
    public class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ToDoItem, ToDoItemResponse>()
                .Map(dest => dest.TaskName, src => src.TaskDescription)
                .Map(dest => dest.IsDone, src => src.IsCompleted);

            // serve altrimenti non si riesce ad eseguire il mapping tra Dto input e Model (ToDoItem) //
            config.ForType<ToDoItemRequest, ToDoItem>()
                .Map(dest => dest.TaskDescription, src => src.TaskName)
                .Map(dest => dest.IsCompleted, src => src.IsDone);
        }
    }
}
 
