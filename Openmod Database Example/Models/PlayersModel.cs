

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

using System.ComponentModel.DataAnnotations;

namespace OpenmodDatabaseExample.Models.Players
{
    public class Players
    {
        [Key]
        [Required]
        public ulong SteamID { get; set; }
    }
}
