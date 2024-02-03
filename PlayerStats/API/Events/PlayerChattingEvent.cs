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

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html
namespace PlayerStats.API.Events
{
    public class PlayerChattingEvent : IEventListener<UnturnedPlayerChattingEvent>
    {

        private readonly IDbService m_DbService;
        public PlayerChattingEvent(IDbService dbService)
        {
            m_DbService = dbService;
        }

        public async Task HandleEventAsync(object? sender, UnturnedPlayerChattingEvent @event)
        {
            AsyncHelper.Schedule("PlayerStats_PlayerChatting", async () =>
            {
                UnturnedPlayer player = @event.Player;
                Stats stats = new Stats
                {
                    PlayerID = player.SteamId.m_SteamID,
                    PlayerName = player.Player.name
                };
                await m_DbService.UpsertAddStatAsync(stats, "Messages");
            });
        }
    }
}
