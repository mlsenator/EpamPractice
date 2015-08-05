using System;
using System.Collections.Generic;

namespace WcfServiceLibrary.Models
{
    public partial class User
    {
        public User()
        {
            this.Tasks = new List<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
