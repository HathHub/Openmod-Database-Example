using System;
using PlayerStats.Models.Stats;
using PlayerStats.API.Services;
using OpenMod.API.Users;
using System.Threading.Tasks;
using OpenMod.Core.Commands;
using OpenMod.Unturned.Users;
using Command = OpenMod.Core.Commands.Command;
using SmartFormat;
using OpenMod.API.Commands;
using Microsoft.Extensions.Configuration;

namespace PlayerStats.Commands
{
    [Command("stats")]
    [CommandDescription("Shows stats for the player")]
    [CommandSyntax("[player]")]
    public class CommandStats : Command
    {
        private readonly IDbService m_DbService;
        private readonly UnturnedUserDirectory m_UnturnedUserDirectory;
        private readonly IConfiguration m_Configuration;
        public CommandStats(IDbService dbService, IServiceProvider serviceProvider, UnturnedUserDirectory unturnedUserDirectory, IConfiguration configuration) : base(serviceProvider)
        {
            m_DbService = dbService;
            m_UnturnedUserDirectory = unturnedUserDirectory;
            m_Configuration = configuration;
        }
        protected override async Task OnExecuteAsync()
        {
            UnturnedUser user;
            var player = await Context.Parameters.GetAsync<string>(0);
            if(player.Length == 0)
            {
                user = (UnturnedUser)Context.Actor;
            }
            else
            {
                UnturnedUser? _user =  m_UnturnedUserDirectory.FindUser(player, UserSearchMode.FindByNameOrId);
                if(_user != null)
                {
                    user = _user;
                }
                else
                {
                    throw new UserFriendlyException("Couldn't find that player");
                }
            }
            Stats stats = await m_DbService.GetPlayerStatWithRankAsync(user.Player.SteamId.m_SteamID);
                if (stats is null) stats = new Stats() {  };
                string message = m_Configuration["Stats:Message"];
            await Context.Actor.PrintMessageAsync(Smart.Format(message, new
                {
                    PlayerName = user.Player.Player.name,
                    Kills = stats.Kills,
                    Deaths = stats.Deaths,
                    Messages = stats.Messages,
                    Kdr = stats.Kills > 0 ? stats.Kills / stats.Deaths * 100 : 0,
                    Headshots = stats.Headshots,
                    Zombies = stats.Zombies,
                    MegaZombies = stats.MegaZombies,
                    Resources = stats.Resources,
                    Harvests = stats.Harvests,
                    Fish = stats.Fish,
                    Animals = stats.Animals,
                    Position = stats.Rank,
                    Accuracy = stats.Kills > 0 ? stats.Headshots / stats.Kills * 100 : 0,
  
                }));
        }
    }
}
