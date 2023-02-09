namespace EntityFrameworkLibrary.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string? TaskDescription { get; set; }
        public string? IsCompleted { get; set; }
    }
}
