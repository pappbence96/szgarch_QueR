using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueR.DAL;
using QueR.DAL.Seed;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.Application.Extensions
{
    public static class HostExtensions
    {
        public static async Task<IHost> CreateAndUpdateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await appDbContext.Database.EnsureCreatedAsync();

            return host;
        }
    }
}
