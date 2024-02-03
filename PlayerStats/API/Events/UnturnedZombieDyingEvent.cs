using Microsoft.Extensions.Logging;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using OpenMod.API.Eventing;
using OpenMod.Unturned.Users;
using System.Text;
using OpenMod.Unturned.Players.Chat.Events;
using PlayerStats.API.Services;
using PlayerStats.Models.Stats;
using SDG.Unturned;
using OpenMod.Unturned.Players;
using UnityEngine.Assertions.Must;
using OpenMod.Core.Helpers;
using System;
using OpenMod.Unturned.Zombies.Events;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html
namespace PlayerStats.API.Events
{
    public class UnturnedZombieDeadEventListener : IEventListener<UnturnedZombieDyingEvent>
    {

        private readonly IDbService m_DbService;
        public UnturnedZombieDeadEventListener(IDbService dbService)
        {
            m_DbService = dbService;
        }

        public async Task HandleEventAsync(object? sender, UnturnedZombieDyingEvent @event)
        {
            AsyncHelper.Schedule("PlayerStats_ZombieDead", async () =>
            {
                if (@event.Instigator!.Player is null) return;
                Stats stats = new Stats
                {
                    PlayerID = @event.Instigator.SteamId.m_SteamID,
                    PlayerName = @event.Instigator.Player.name,
                };
                await m_DbService.UpsertAddStatAsync(stats, "Zombies");
            });
        }
    }
}
