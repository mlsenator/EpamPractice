using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WorstToDoManager.Models.Mapping;

namespace WorstToDoManager.Models
{
    public partial class SlowDBContext : DbContext
    {
        static SlowDBContext()
        {
            Database.SetInitializer<SlowDBContext>(null);
        }

        public SlowDBContext()
            : base("Name=SlowDBContext")
        {
        }

        public DbSet<ToDoTask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TaskMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
