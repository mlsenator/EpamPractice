using System;
using System.Collections.Generic;

namespace WcfServiceLibrary.Models
{
    public partial class Task : ToDo.Infrastructure.IToDoItem
    {
        public int ToDoId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool IsCompleted { get; set; }
        public virtual User User { get; set; }
    }
}
