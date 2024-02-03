using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerStats.Models.Stats
{
    public class Stats
    {
        [Key]
        public ulong PlayerID { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public int Zombies { get; set; }
        public int Messages { get; set; }
        public int Deaths { get; set; }
        public int Headshots { get; set; }
        public int Kills { get; set; }
        public int MegaZombies { get; set; }
        public int Resources { get; set; }
        public int Harvests { get; set; }
        public int Fish { get; set; }
        public int Animals { get; set; }
        public DateTime LastUpdated { get; set; }
        [NotMapped]
        public int Rank { get; set; }

    }
}
