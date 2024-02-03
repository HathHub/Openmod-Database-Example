// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenMod.API.Ioc;
using OpenMod.API.Prioritization;
using PlayerStats.Databases;
using PlayerStats.Models.Stats;
using System.Linq;
using System.Collections.Generic;

namespace PlayerStats.API.Services
{
    [PluginServiceImplementation(Lifetime = ServiceLifetime.Singleton, Priority = Priority.Lowest)]
    public class DbService : IDbService
    {
        private readonly ILogger<PlayerStats> m_Logger;
        private readonly IServiceProvider m_ServiceProvider;
        public DbService(IServiceProvider serviceProvider, ILogger<PlayerStats> logger)
        {
            m_ServiceProvider = serviceProvider;
            m_Logger = logger;
            m_Logger.LogInformation("Creating database context");
        }

        public DatabaseContext GetDbContext()
        {
            return m_ServiceProvider.GetRequiredService<DatabaseContext>();
        }
        private async Task RunOperation(Func<DatabaseContext, Task> action)
        {
            await using var dbContext = GetDbContext();

            await action(dbContext);
        }

        private async Task<T> RunOperation<T>(Func<DatabaseContext, Task<T>> action)
        {
            await using var dbContext = GetDbContext();

            return await action(dbContext);
        }

        private void RunOperation(Action<DatabaseContext> action)
        {
            using var dbContext = GetDbContext();

            action(dbContext);
        }
        public async Task UpsertAddStatAsync(Stats stats, string type)
        {
            await RunOperation(async dbContext =>
            {
                string sql = $@"
                    INSERT INTO `PlayerStats_Players` (PlayerID, PlayerName, {type}, LastUpdated) 
                    VALUES ({{{0}}}, {{{1}}}, 1, NOW()) 
                    ON DUPLICATE KEY UPDATE 
                        PlayerName = VALUES(PlayerName), 
                        {type} = {type} + 1,
                        LastUpdated = NOW();";
                await dbContext.Database.ExecuteSqlRawAsync(sql, stats.PlayerID, stats.PlayerName);
            });
        }
        public async Task<Stats> GetPlayerStatAsync(ulong PlayerID)
        {
            return await RunOperation(async dbContext =>
            {
                Stats? _stats = await dbContext.Players
                .Where(player => player.PlayerID == PlayerID)
                .FirstOrDefaultAsync();
                return _stats;
            });
        }
        public async Task<Stats> GetPlayerStatWithRankAsync(ulong PlayerID)
        {
            return await RunOperation(async dbContext =>
            {
                var orderedPlayers = await dbContext.Players
                    .OrderByDescending(player => player.Kills)
                    .ToListAsync();

                Stats playerStats = orderedPlayers.FirstOrDefault(player => player.PlayerID == PlayerID);

                playerStats.Rank = orderedPlayers.FindIndex(player => player.PlayerID == PlayerID) + 1;

                return playerStats;
            });
        }
        public async Task<List<Stats>> GetPlayersStatRankedAsync()
        {
            return await RunOperation(async dbContext =>
            {
                var orderedPlayers = await dbContext.Players
                    .OrderByDescending(player => player.Kills)
                    .Take(10) // Take the top 10 players
                    .ToListAsync();

                int rank = 1;
                List<Stats> top10PlayerStats = orderedPlayers.Select(player => new Stats
                {
                    PlayerID = player.PlayerID,
                    PlayerName = player.PlayerName,
                    Kills = player.Kills,
                    Rank = rank++
                }).ToList();

                return top10PlayerStats;
            });
        }

    }
}
