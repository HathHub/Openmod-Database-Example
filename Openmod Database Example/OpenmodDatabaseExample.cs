using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NuGet.Protocol;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;
using OpenmodDatabaseExample.Databases;
using OpenmodDatabaseExample.Models.Players;
using SDG.Unturned;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

[assembly: PluginMetadata("OpenmodDatabaseExample", DisplayName = "My OpenMod Plugin", Author = "Hath.")]

namespace OpenmodDatabaseExample
{
    public class OpenmodDatabaseExample : OpenModUnturnedPlugin
    {
        private readonly IConfiguration m_Configuration;
        private readonly IStringLocalizer m_StringLocalizer;
        private readonly ILogger<OpenmodDatabaseExample> m_Logger;
        private readonly IServiceProvider m_ServiceProvider;

        public OpenmodDatabaseExample(
            IConfiguration configuration,
            IStringLocalizer stringLocalizer,
            ILogger<OpenmodDatabaseExample> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            m_Configuration = configuration;
            m_StringLocalizer = stringLocalizer;
            m_ServiceProvider = serviceProvider;
            m_Logger = logger;
        }

        protected override async UniTask OnLoadAsync()
        {
            m_Logger.LogInformation("Hello World!");
            await using var dbContext = m_ServiceProvider.GetRequiredService<DatabaseContext>();
            await dbContext.Database.MigrateAsync();
            Players players = new Players
            {
                SteamID = 12332
            };
            //await dbContext.Players.AddAsync(players);
            await dbContext.SaveChangesAsync();
            Players? _players = await dbContext.Players
            .Where(x => x.SteamID == 12332)
            .FirstOrDefaultAsync();
            if (_players is null)
            {
                m_Logger.LogInformation("Couldn't find the user.");
            }
            else
            {
                m_Logger.LogInformation(_players.ToJson().ToString());
            }
        }

        protected override async UniTask OnUnloadAsync()
        {
            // await UniTask.SwitchToMainThread();
            await UniTask.SwitchToMainThread();
            await UniTask.SwitchToTaskPool();
            await UniTask.SwitchToThreadPool();
            m_Logger.LogInformation(m_StringLocalizer["plugin_events:plugin_stop"]);
        }
    }
}
