using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using EntityFrameworkLibrary.Context;
using EntityFrameworkLibrary.UnitOfWorks;
using ToDoListFunc.Startup;
using System;
using ToDoListFunc.Services;

[assembly: FunctionsStartup(typeof(MyStartup))]

namespace ToDoListFunc.Startup
{
    public class MyStartup : FunctionsStartup
    {
        public MyStartup()
        {

        }

        // Configure Method to configure hosting (IUnitOfWork) // 
        public override void Configure(IFunctionsHostBuilder service)
        {
            //Possiamo rimuovere questa stringa per sostituirla con quella di UnitOfWork dato che il suo compito è quello di raccogliere
            //le repository da iniettare.
            //service.Services.AddScoped<IToDoListRepository, ToDoListRepository>();

            //Iniezione delle dipendenze con Scoped
            service.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            service.Services.AddScoped<IToDoService, ToDoService>();


            //Creazione del contesto nel database
            //string connectionString = Environment.GetEnvironmentVariable("ConnectionString");

            //service.Services.AddDbContext<ToDoListDbContext>(options => options.UseSqlServer(connectionString));
            service.Services.AddDbContext<ToDoListDbContext>();
        }
    }
}
