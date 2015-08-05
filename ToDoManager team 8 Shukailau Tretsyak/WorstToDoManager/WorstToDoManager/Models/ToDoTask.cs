using System;
using System.Collections.Generic;
using ToDo.Infrastructure;

namespace WorstToDoManager.Models
{
    public partial class ToDoTask : IToDoItem
    {
        public int ToDoId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
