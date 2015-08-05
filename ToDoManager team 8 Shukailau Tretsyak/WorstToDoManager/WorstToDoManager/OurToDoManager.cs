using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ToDo.Infrastructure;
using WorstToDoManager.Models;
using WorstToDoManager.DefaultService;

namespace WorstToDoManager
{
    public class OurToDoManager : ToDo.Infrastructure.IToDoManager
    {
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
        public void CreateToDoItem(IToDoItem todo)
        {
            SlowDBContext c = new SlowDBContext();
            c.Tasks.Add(ToTaskMapper(todo));
            c.SaveChanges();
            DefaultService.ToDoManagerClient service = new DefaultService.ToDoManagerClient();
            service.CreateToDoItemAsync(ToItemMapper(todo));
        }
        public void DeleteToDoItem(int todoItemId)
        {
            DefaultService.ToDoManagerClient service = new DefaultService.ToDoManagerClient();
            service.DeleteToDoItemAsync(todoItemId);
            SlowDBContext c = new SlowDBContext();
            var t = c.Tasks.FirstOrDefault(u => u.ToDoId == todoItemId);
            if (t != null)
            {
                c.Tasks.Remove(t);
                c.SaveChanges();
            }

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
        public List<IToDoItem> GetTodoList(int userId)
        {
            SlowDBContext c = new SlowDBContext();
            var list = c.Tasks.Where(x => x.UserId == userId).ToList();
            var newList = new List<IToDoItem>();
            foreach (var i in list)
                newList.Add((IToDoItem)i);

            //AsincGetTodoList(userId);

            return newList;
        }
        //async public void AsincGetTodoList(int userId)
        //{
        //    DefaultService.ToDoManagerClient service = new DefaultService.ToDoManagerClient();
        //    var list = await service.GetTodoListAsync(userId);
        //    var taskList = new List<ToDoTask>();
        //    foreach (var l in list)
        //        taskList.Add(ItemToTaskMapper(l));

        //    SlowDBContext c = new SlowDBContext();

        //    foreach (var entity in c.Tasks)
        //    {
        //        var t = taskList.FirstOrDefault(u => u.ToDoId == entity.ToDoId);

        //        if (t == null)
        //        {
        //            c.Tasks.Remove(entity);
        //            taskList.Remove(t);
        //        }
        //        else
        //        {
        //            var x = c.Set<ToDoTask>().Find(t.ToDoId);
        //            c.Entry(x).CurrentValues.SetValues(t);
        //            taskList.Remove(t);
        //        }   
        //    }
        //    foreach (var entity in taskList)
        //        c.Tasks.Add(entity);
        //    c.SaveChanges();
        //}

    #region mappers
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
        private ToDoTask ItemToTaskMapper (ToDoItem item)
        {
            ToDoTask t = new ToDoTask();
            t.IsCompleted = item.IsCompleted;
            t.Name = item.Name;
            t.ToDoId = item.ToDoId;
            t.UserId = item.UserId;
            return t;
        }
    #endregion
    }
}
