using System;
using System.Collections.Generic;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NorthWind.Entities.Exceptions;
using NorthWind.IoC;
using NorthWind.WebExceptionPresenter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(
    new ApiExceptionFilterAttribute(
        new Dictionary<Type, IExceptionHandler>
        {
            {typeof(GeneralException),new GeneralExceptionHandler()},
            {typeof(ValidationException),new ValidationExceptionHandler()}
        }
    )));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "NorthWind.WebApi", Version = "v1" });
});
builder.Services.AddNorthWindServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NorthWind.WebApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
