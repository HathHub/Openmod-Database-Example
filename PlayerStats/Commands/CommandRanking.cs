using System;
using PlayerStats.Models.Stats;
using PlayerStats.API.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using OpenMod.Core.Commands;
using Command = OpenMod.Core.Commands.Command;
using SmartFormat;
using Microsoft.Extensions.Configuration;

namespace PlayerStats.Commands
{
    [Command("ranking")]
    [CommandDescription("Shows ranking of stats for the server")]
    public class CommandRanking : Command
    {
        private readonly IDbService m_DbService;
        private readonly IConfiguration m_Configuration;
        public CommandRanking(IDbService dbService, IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
        {
            m_DbService = dbService;
            m_Configuration = configuration;
        }
        protected override async Task OnExecuteAsync()
        {
            List<Stats> _stats = await m_DbService.GetPlayersStatRankedAsync();
            string message = m_Configuration["Ranking:Header"];
            foreach( Stats stats in _stats)
            {
                message += Smart.Format(m_Configuration["Ranking:Message"], new
                {
                    PlayerName = stats.PlayerName,
                    Kills = stats.Kills,
                    Deaths = stats.Deaths,
                    Messages = stats.Messages,
                    Kdr = stats.Deaths > 0 ? stats.Kills / stats.Deaths : 0,
                    Headshots = stats.Headshots,
                    Zombies = stats.Zombies,
                    MegaZombies = stats.MegaZombies,
                    Resources = stats.Resources,
                    Harvests = stats.Harvests,
                    Fish = stats.Fish,
                    Animals = stats.Animals,
                    Position = stats.Rank,
                    Accuracy = stats.Kills > 0 ? stats.Headshots / stats.Kills * 100 : 0,

                });
            }
            await Context.Actor.PrintMessageAsync(message);
        }
    }
}
