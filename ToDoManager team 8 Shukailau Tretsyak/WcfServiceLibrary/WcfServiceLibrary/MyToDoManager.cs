using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDo.Infrastructure;
using WcfServiceLibrary.Models;

namespace WcfServiceLibrary
{
    public class MyToDoManager : ToDo.Infrastructure.IToDoManager
    {
        public void CreateToDoItem(IToDoItem todo)
        {
            RIDBContext c = new RIDBContext();
            c.Tasks.Add(ToTaskMapper(todo));
            c.SaveChanges();
        }

        public int CreateUser(string name)
        {
            DefaultService.ToDoManagerClient service = new DefaultService.ToDoManagerClient();
            RIDBContext c = new RIDBContext();
            int id = service.CreateUser(name);
            c.Users.Add(new User() {Name = name, Id = id});
            c.SaveChanges();
            return id;

        }
        public void DeleteToDoItem(int todoItemId)
        {
            RIDBContext c = new RIDBContext();
            var t = c.Tasks.FirstOrDefault(u => u.ToDoId == todoItemId);
            if (t != null)
            {
                c.Tasks.Remove(t);
                c.SaveChanges();
            }
        }
        public List<IToDoItem> GetTodoList(int userId)
        {
            RIDBContext c = new RIDBContext();
            var list = c.Tasks.Where(x => x.UserId == userId).ToList();
            var newList = new List<IToDoItem>();
            foreach (var i in list)
                newList.Add(ToItemMapper(i));
            return newList;
        }
        public void UpdateToDoItem(IToDoItem todo)
        {
            RIDBContext c = new RIDBContext();
            var x = c.Set<Task>().Find(todo.ToDoId);
            c.Entry(x).CurrentValues.SetValues(todo);
            c.SaveChanges();
        }

        private Task ToTaskMapper (IToDoItem item)
        {
            Task t = new Task();
            t.IsCompleted = item.IsCompleted;
            t.Name = item.Name;
            t.ToDoId = item.ToDoId;
            t.UserId = item.UserId;
            return t;
        }

        private IToDoItem ToItemMapper(Task t)
        {
            IToDoItem item = new Task();
            item.IsCompleted = t.IsCompleted;
            item.Name = t.Name;
            item.ToDoId = t.ToDoId;
            item.UserId = t.UserId;
            return item;
        }

    }
}
