using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EntityFrameworkLibrary.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace ToDoListFunc
{
    // Questa classe è utile per risolvere il problema dell'add-migration per Azure Function //
    public class ToDoListDbContextFactory : IDesignTimeDbContextFactory<ToDoListDbContext>
    {
        public ToDoListDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var keyVaultUri = configuration.GetValue<string>("KVaultUri");
            var secretClient = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
            var connectionString = secretClient.GetSecret("AzureConnString-AF").Value.Value;

            var optionBuilder = new DbContextOptionsBuilder<ToDoListDbContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new ToDoListDbContext(optionBuilder.Options);
        }
    }
}
