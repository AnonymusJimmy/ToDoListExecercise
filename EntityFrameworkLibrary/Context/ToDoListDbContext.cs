using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkLibrary.Models;
using EntityFrameworkLibrary.Repositories;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var keyVaultUri = Environment.GetEnvironmentVariable("KVaultUri");
            var secretClient = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
            string secret = secretClient.GetSecret("ConnectionString-AF").Value.Value;

            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(secret);
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
