using System;
using Cysharp.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OpenMod.API.Plugins;
using OpenMod.EntityFrameworkCore.Configurator;
using OpenMod.EntityFrameworkCore;
using OpenMod.Unturned.Plugins;
using OpenmodDatabaseExample.Models.Players;
using Microsoft.Extensions.DependencyInjection;
using OpenMod.API.Ioc;
using OpenMod.API.Prioritization;

namespace OpenmodDatabaseExample.Databases
{
    [PluginServiceImplementation(Lifetime = ServiceLifetime.Singleton, Priority = Priority.Lowest)]
    public class DatabaseContext : OpenModDbContext<DatabaseContext>
    {
        private readonly IServiceProvider m_ServiceProvider;
        public DatabaseContext(IDbContextConfigurator configurator, IServiceProvider serviceProvider) : base(configurator, serviceProvider)
        {
            m_ServiceProvider = serviceProvider;
        }
        private DatabaseContext GetDbContext()
        {
            return m_ServiceProvider.GetRequiredService<DatabaseContext>();
        }
        public DbSet<Players> Players => Set<Players>();
    }
}
