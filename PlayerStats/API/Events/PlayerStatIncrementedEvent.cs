using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using OpenMod.API.Eventing;
using OpenMod.Unturned.Users;
using OpenMod.Unturned.Locations;
using System.Text;
using OpenMod.Unturned.Players.Stats.Events;
using SDG.Unturned;
using OpenMod.Core.Helpers;
using PlayerStats.API.Services;
using PlayerStats.Models.Stats;

namespace PlayerStats.API.Events
{
    public class StatIncremented : IEventListener<UnturnedPlayerStatIncrementedEvent>
    {
        private readonly IDbService m_DbService;
        public StatIncremented(IDbService dbService)
        {
            m_DbService = dbService;
        }

        public async Task HandleEventAsync(object? sender, UnturnedPlayerStatIncrementedEvent @event)
        {
            AsyncHelper.Schedule("PlayerStats_StatIncremented", async () =>
            {
                Stats stats = new Stats
                {
                    PlayerID = @event.Player.SteamId.m_SteamID,
                    PlayerName = @event.Player.Player.name
                };
                switch (@event.Stat)
                {
                    case EPlayerStat.FOUND_FISHES:
                        await m_DbService.UpsertAddStatAsync(stats, "Fish");
                        break;
                    case EPlayerStat.KILLS_ANIMALS:
                        await m_DbService.UpsertAddStatAsync(stats, "Animals");
                        break;
                    case EPlayerStat.FOUND_RESOURCES:
                        await m_DbService.UpsertAddStatAsync(stats, "Resources");
                        break;
                    case EPlayerStat.KILLS_ZOMBIES_NORMAL:
                        await m_DbService.UpsertAddStatAsync(stats, "Zombies");
                        break;
                    case EPlayerStat.KILLS_ZOMBIES_MEGA:
                        await m_DbService.UpsertAddStatAsync(stats, "MegaZombies");
                        break;
                    case EPlayerStat.FOUND_PLANTS:
                        await m_DbService.UpsertAddStatAsync(stats, "Harvests");
                        break;
                }
                
            });

        }
    }
}
