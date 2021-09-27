﻿using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthWind.Entities.Interfaces;
using NorthWind.Repositories.EFCore.Datacontext;
using NorthWind.Repositories.EFCore.Repositories;
using NorthWind.UseCases.Common.Behaviors;
using NorthWind.UseCases.CreateOrder;

namespace NorthWind.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddNorthWindServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<NorthWindContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("NorthWindDB")));
            services.AddScoped<IOrderRepositoy, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMediatR(typeof(CreateOrderInteractor));
            services.AddValidatorsFromAssembly(typeof(CreateOrderValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}