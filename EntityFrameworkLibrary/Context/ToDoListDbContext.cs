using EntityFrameworkLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkLibrary.Context
{
    public class ToDoListDbContext : DbContext
    {
        //Il Costruttore vuoto è ESSENZIALE in quanto, quando viene lanciata una nuova migrazione, l'applicazione
        //deve permettere a progetti/library esterni di creare nuove configurazioni senza specificare alcuna connessione.
        public ToDoListDbContext()
        {

        }

        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {

        }

        
        //Construction of the Sql tables
        public virtual DbSet<ToDoItem> ToDoItems { get; set; }


        // OnConfigure Method (Used for Add-Migration) //
       


        /*OnModelCreating method for tell to EF how the DB SQL  should be done*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>(entity =>
            {
                entity.ToTable("Tasks");
                entity.Property(e => e.TaskDescription).HasColumnType("nvarchar").HasMaxLength(50);
                entity.Property(e => e.IsCompleted).HasColumnType("nvarchar").HasMaxLength(5);
            });
            
            base.OnModelCreating(modelBuilder);
        }        
    }
}
