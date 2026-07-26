using System;
using System.Collections.Generic;
using System.Text;
using AgendTime.Domain.Interfaces;
using AgendTime.Infrastructure.Data;
using AgendTime.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgendTime.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IClientRepository, ClientRepository>();

        return services;
    }
}