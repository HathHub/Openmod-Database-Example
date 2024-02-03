using System;
using Microsoft.EntityFrameworkCore;
using OpenMod.EntityFrameworkCore.Configurator;
using OpenMod.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenMod.API.Ioc;
using OpenMod.API.Prioritization;
using PlayerStats.Models.Stats;
using PlayerStats.API.Services;
using OpenMod.API.Users;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using NuGet.Protocol.Plugins;

namespace PlayerStats.Databases
{
    //[PluginServiceImplementation(Lifetime = ServiceLifetime.Scoped, Priority = Priority.Lowest)]
    public class DatabaseContext : OpenModDbContext<DatabaseContext>
    {
        public DbSet<Stats> Players => Set<Stats>();
        public DatabaseContext(IDbContextConfigurator configurator, IServiceProvider serviceProvider) : base(configurator, serviceProvider)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Stats>()
                .Property(b => b.Zombies)
                .HasDefaultValue(0);

            modelBuilder.Entity<Stats>()
                .Property(b => b.Messages)
                .HasDefaultValue(0);

            modelBuilder.Entity<Stats>()
                .Property(b => b.Deaths)
                .HasDefaultValue(0);

            modelBuilder.Entity<Stats>()
                .Property(b => b.Headshots)
                .HasDefaultValue(0);

            modelBuilder.Entity<Stats>()
                .Property(b => b.Kills)
                .HasDefaultValue(0);

            modelBuilder.Entity<Stats>()
                .Property(b => b.MegaZombies)
                .HasDefaultValue(0);

            modelBuilder.Entity<Stats>()
                .Property(b => b.Resources)
                .HasDefaultValue(0);

            modelBuilder.Entity<Stats>()
                .Property(b => b.Harvests)
                .HasDefaultValue(0);

            modelBuilder.Entity<Stats>()
                .Property(b => b.Fish)
                .HasDefaultValue(0);

            modelBuilder.Entity<Stats>()
                .Property(b => b.Animals)
                .HasDefaultValue(0);

        }

    }
}
