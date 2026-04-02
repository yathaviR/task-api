using Microsoft.EntityFrameworkCore;
using TaskApi.Models;

namespace TaskApi.Data
{
    //Database context for Task Management API
    //Manages all database operations and entity mappings

    public class TaskDbContext : DbContext
    {
        //Constructor accepting DbContext options
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        //DbSet for TodoTask Entities
        //Maps to the Tasks table in the database
        public DbSet<TodoTask> Tasks { get; set; }

        //Called when the model is being created
        //Configure entity mappings and constraints here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configure the TodoTask entity
            modelBuilder.Entity<TodoTask>(entity =>
            {
                //Map to Tasks table
                entity.ToTable("Tasks");

                //Primary key
                entity.HasKey(e => e.Id);

                //Configure properties
                entity.Property(e => e.Title)
                    .IsRequired() //Title is required
                    .HasMaxLength(200) //Max length for Title
                    .HasColumnName("Title"); //Column name in database)

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)  //Max length for Description
                    .HasColumnName("Description"); //Column name in database

                entity.Property(e => e.IsCompleted)
                    .HasDefaultValue(false) //Default value for IsCompleted
                    .HasColumnName("IsCompleted"); //Column name in database

                entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()") //Default value for CreatedAt
                    .HasColumnName("CreatedAt"); //Column name in database

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("UpdatedAt"); //Column name in database

                entity.HasIndex(e=>e.IsCompleted)
                    .HasDatabaseName("IDX_Tasks_IsCompleted"); //Index on IsCompleted for faster queries  Create indexes for performance

                entity.HasIndex(e => e.CreatedAt)
                    .HasDatabaseName("IDX_Tasks_CreatedAt"); //Index on CreatedAt for faster queries
            });
        }


    }
}
