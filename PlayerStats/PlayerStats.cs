using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NuGet.Protocol;
using OpenMod.API.Plugins;
using OpenMod.Core.Helpers;
using OpenMod.Unturned.Players;
using OpenMod.Unturned.Plugins;
using PlayerStats.API.Services;
using PlayerStats.Commands;
using PlayerStats.Databases;
using PlayerStats.Models.Stats;
using SDG.Unturned;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

[assembly: PluginMetadata("PlayerStats", DisplayName = "Player Stats", Author = "Hath.")]

namespace PlayerStats
{
    public class PlayerStats : OpenModUnturnedPlugin
    {
        private readonly IConfiguration m_Configuration;
        private readonly IStringLocalizer m_StringLocalizer;
        private readonly ILogger<PlayerStats> m_Logger;
        private readonly IServiceProvider m_ServiceProvider;
        private readonly IDbService m_DbService;

        public PlayerStats(
            IConfiguration configuration,
            IStringLocalizer stringLocalizer,
            ILogger<PlayerStats> logger,
            IServiceProvider serviceProvider,
            IDbService dbService) : base(serviceProvider)
        {
            m_Configuration = configuration;
            m_StringLocalizer = stringLocalizer;
            m_ServiceProvider = serviceProvider;
            m_Logger = logger;
            m_DbService = dbService;
        }

        protected override async UniTask OnLoadAsync()
        {
            try
            {
                m_Logger.LogInformation("Hello World!");

                await using var dbContext = m_ServiceProvider.GetRequiredService<DatabaseContext>();
                await dbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                m_Logger.LogError($"Error during database initialization: {ex}");
            }
        }
    }
}
