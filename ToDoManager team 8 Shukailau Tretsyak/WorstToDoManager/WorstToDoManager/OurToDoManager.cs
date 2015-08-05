using System.Collections.Generic;
using System.Linq;
using ToDo.Infrastructure;
using WorstToDoManager.Models;
using WorstToDoManager.DefaultService;

namespace WorstToDoManager
{
    public class OurToDoManager : ToDo.Infrastructure.IToDoManager
    {
        public void CreateToDoItem(IToDoItem todo)
        {
            SlowDBContext c = new SlowDBContext();
            DefaultService.ToDoManagerClient service = new DefaultService.ToDoManagerClient();
            service.CreateToDoItemAsync(ToItemMapper(todo));
            c.Tasks.Add(ToTaskMapper(todo));
            c.SaveChanges();
        }

        public int CreateUser(string name)
        {
            DefaultService.ToDoManagerClient service = new DefaultService.ToDoManagerClient();
            SlowDBContext c = new SlowDBContext();
            int id = service.CreateUser(name);
            var user = new User() { Name = name, Id = id };
            c.Users.Add(user);
            c.SaveChanges();
            return id;

        }
        public void DeleteToDoItem(int todoItemId)
        {
            SlowDBContext c = new SlowDBContext();
            var t = c.Tasks.FirstOrDefault(u => u.ToDoId == todoItemId);
            if (t != null)
            {
                c.Tasks.Remove(t);
                c.SaveChanges();
            }
            DefaultService.ToDoManagerClient service = new DefaultService.ToDoManagerClient();
            service.DeleteToDoItemAsync(todoItemId);

        }
        public List<IToDoItem> GetTodoList(int userId)
        {
            SlowDBContext c = new SlowDBContext();
            var list = c.Tasks.Where(x => x.UserId == userId).ToList();
            var newList = new List<IToDoItem>();
            foreach (var i in list)
                newList.Add((IToDoItem)i);
            return newList;
        }
        public void UpdateToDoItem(IToDoItem todo)
        {
            SlowDBContext c = new SlowDBContext();
            var x = c.Set<ToDoTask>().Find(todo.ToDoId);
            c.Entry(x).CurrentValues.SetValues(todo);
            c.SaveChanges();
            DefaultService.ToDoManagerClient service = new DefaultService.ToDoManagerClient();
            service.UpdateToDoItemAsync(ToItemMapper(todo));
        }

        private ToDoTask ToTaskMapper(IToDoItem item)
        {
            ToDoTask t = new ToDoTask();
            t.IsCompleted = item.IsCompleted;
            t.Name = item.Name;
            t.ToDoId = item.ToDoId;
            t.UserId = item.UserId;
            return t;
        }

        private ToDoItem ToItemMapper(IToDoItem item)
        {
            ToDoItem t = new ToDoItem();
            t.IsCompleted = item.IsCompleted;
            t.Name = item.Name;
            t.ToDoId = item.ToDoId;
            t.UserId = item.UserId;
            return t;
        }
    }
}
