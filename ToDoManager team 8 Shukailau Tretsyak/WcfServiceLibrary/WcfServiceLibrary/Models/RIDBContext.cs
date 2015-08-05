using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WcfServiceLibrary.Models.Mapping;

namespace WcfServiceLibrary.Models
{
    public partial class RIDBContext : DbContext
    {
        static RIDBContext()
        {
            Database.SetInitializer<RIDBContext>(null);
        }

        public RIDBContext()
            : base("Name=RIDBContext")
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TaskMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
