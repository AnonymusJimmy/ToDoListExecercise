using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkLibrary.Models;
using EntityFrameworkLibrary.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkLibrary.Context
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext()
        {

        }

        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {

        }
        

        public DbSet<ToDoItem> ToDoItems { get; set; }


        // OnConfigure Method (Used for Add-Migration) //
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ConnectionString");
            }
        }


        /*OnModelCreating method for tell to EF how the DB SQL  should be done*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>(entity =>
            {
                entity.ToTable("Tasks");
                entity.Property(e => e.TaskName).HasColumnType("nvarchar").HasMaxLength(50);
                entity.Property(e => e.IsCompleted).HasColumnType("nvarchar").HasMaxLength(5);
            });

            //modelBuilder.Entity<ToDoItem>()
            //    .HasKey(e => e.Id);
            
            //OnModelCreating(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }        
    }
}
