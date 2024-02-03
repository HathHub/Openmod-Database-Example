using Microsoft.Extensions.Logging;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using OpenMod.API.Eventing;
using OpenMod.Unturned.Users;
using OpenMod.Unturned.Players.Life.Events;
using System.Text;
using SDG.Unturned;
using OpenMod.Core.Helpers;
using OpenMod.Unturned.Players;
using PlayerStats.Models.Stats;
using PlayerStats.API.Services;
using System;

namespace PlayerStats.API.Events
{
    public class PlayerDeathEvent : IEventListener<UnturnedPlayerDeathEvent>
    {
        private readonly IDbService m_DbService;
        private readonly UnturnedUserDirectory m_UnturnedUserDirectory;
        public PlayerDeathEvent(IDbService dbService, UnturnedUserDirectory unturnedUserDirectory)
        {
            m_DbService = dbService;
            m_UnturnedUserDirectory = unturnedUserDirectory;
        }
        public async Task HandleEventAsync(object? sender, UnturnedPlayerDeathEvent @event)
        {
            AsyncHelper.Schedule("PlayerStats_PlayerDeath", async () =>
            {
                UnturnedPlayer victim = @event.Player;
                Stats _victim = new Stats
                {
                    PlayerID = victim.SteamId.m_SteamID,
                    PlayerName = victim.Player.name
                };
                await m_DbService.UpsertAddStatAsync(_victim, "Deaths");
                UnturnedUser? killer = m_UnturnedUserDirectory.FindUser(@event.Instigator);
                if (@event.Instigator != @event.Player.SteamId && killer is not null)
                {
                    Stats _killer = new Stats
                    {
                        PlayerID = @event.Instigator.m_SteamID,
                    };
                    await m_DbService.UpsertAddStatAsync(_killer, "Kills");
                    if(@event.Limb == ELimb.SKULL) await m_DbService.UpsertAddStatAsync(_killer, "Headshots");
                }
            });
        }
    }
}
