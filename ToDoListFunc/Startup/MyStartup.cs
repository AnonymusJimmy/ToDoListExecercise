using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using EntityFrameworkLibrary.Context;
using EntityFrameworkLibrary.UnitOfWorks;
using Mapster;
using MapsterMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;
using ToDoListFunc.Services;

//[assembly: FunctionsStartup(typeof(MyStartup))]

namespace ToDoListFunc.Startup
{
    public class MyStartup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //Possiamo rimuovere questa stringa per sostituirla con quella di UnitOfWork dato che il suo compito è quello di raccogliere
            //le repository da iniettare.
            //service.Services.AddScoped<IToDoListRepository, ToDoListRepository>();

            //  Register Mapster  //
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            builder.Services.AddSingleton(config);
            builder.Services.AddScoped<IMapper, ServiceMapper>();

            // Swagger Configuration //
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly(), opts =>
            {
                opts.AddCodeParameter = true;
                opts.Documents = new[]
                {
                    new SwaggerDocument
                    {
                        Name = "v1",
                        Title = "Swagger document",
                        Description = "Integrate SwaggerUI with Azure Function",
                        Version = "v2"
                    }
                };
                opts.ConfigureSwaggerGen = x =>
                {
                    x.CustomOperationIds(apiDesc =>
                    {
                        return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : default(Guid).ToString();
                    });
                };
            });

            //Configuration Swagger(builder)//
            ConfigureSwagger(builder);

            //Register UnitOfWork and Service
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IToDoService, ToDoService>();

            //Creazione del contesto nel database
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            builder.Services.AddDbContext<ToDoListDbContext>(options => options.UseSqlServer(connectionString));
        }

        private void ConfigureSwagger(IFunctionsHostBuilder builder)
        {
            /*Register the Swagger Generator*/
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo Api", Version = "v1" });
            });
        }
    }
}
