// Copyright (c) Jan Kubovic All rights reserved.
// Program.cs

namespace DevicesLogger;

using DevicesLogger.Core;
using DevicesLogger.Domain.Devices;
using DevicesLogger.Services;
using MediatR.Extensions.FluentValidation.AspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        _ = builder.Services.AddMemoryCache();
        _ = builder.Services.AddSingleton<IDevicesService, DevicesService>();
        _ = builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Program).Assembly));
        _ = builder.Services.AddFluentValidation(new[]
        {
            typeof(Program).Assembly
        });
        _ = builder.Services.AddControllers(options =>
        {
            _ = options.Filters.Add<ExceptionFilter>();
        });
        //.AddNewtonsoftJson(options =>
        //{
        //    options.SerializerSettings.Converters.Add(
        //            JsonSubtypesConverterBuilder
        //            .Of(typeof(Device), typeof(Device).ToString())
        //            .RegisterSubtype(typeof(Scale), typeof(Scale))
        //            .RegisterSubtype(typeof(Thermometer), typeof(Thermometer))
        //            .SerializeDiscriminatorProperty()
        //            .Build());
        //});

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        _ = builder.Services.AddEndpointsApiExplorer();
        _ = builder.Services.AddSwaggerGen(options =>
        {
            //options.UseAllOfToExtendReferenceSchemas();
            //options.UseAllOfForInheritance();
            //options.UseOneOfForPolymorphism();
            //options.SelectDiscriminatorNameUsing(type =>
            // {
            //     return type.Name switch
            //     {
            //         nameof(Device) => "Device",
            //         _ => null
            //     };
            // });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI();
        }

        _ = app.UseHttpsRedirection();

        _ = app.UseAuthorization();


        _ = app.MapControllers();

        app.Run();
    }
}
