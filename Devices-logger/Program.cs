// Copyright (c) Jan Kubovic All rights reserved.
// Program.cs

namespace DevicesLogger;

using DevicesLogger.Core;
using DevicesLogger.Services;
using MediatR.Extensions.FluentValidation.AspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<IDevicesService, DevicesService>();
        builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Program).Assembly));
        builder.Services.AddFluentValidation(new[]
        {
            typeof(Program).Assembly
        });
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionFilter>();
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
