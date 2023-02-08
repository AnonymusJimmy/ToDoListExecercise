using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkLibrary.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string IsCompleted { get; set; }
    }
}
