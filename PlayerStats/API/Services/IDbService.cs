using System.Collections.Generic;
using System.Threading.Tasks;
using OpenMod.API.Ioc;
using PlayerStats.Commands;
using PlayerStats.Databases;
using PlayerStats.Models.Stats;

namespace PlayerStats.API.Services
{
    [Service]
    public interface IDbService
    {
        DatabaseContext GetDbContext();
        Task UpsertAddStatAsync(Stats stats, string type);
        Task<Stats> GetPlayerStatAsync(ulong PlayerID);
        Task<Stats> GetPlayerStatWithRankAsync(ulong PlayerID);
        Task<List<Stats>> GetPlayersStatRankedAsync();
    }
}
