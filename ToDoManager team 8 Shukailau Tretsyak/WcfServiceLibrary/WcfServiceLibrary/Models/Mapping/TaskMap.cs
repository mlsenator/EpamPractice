using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WcfServiceLibrary.Models.Mapping
{
    public class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            // Primary Key
            this.HasKey(t => t.ToDoId);

            // Properties
            this.Property(t => t.ToDoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Tasks");
            this.Property(t => t.ToDoId).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Task");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.IsCompleted).HasColumnName("Completed");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Tasks)
                .HasForeignKey(d => d.UserId);

        }
    }
}
