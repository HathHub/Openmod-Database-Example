using OpenMod.EntityFrameworkCore.MySql;



// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

namespace PlayerStats.Databases
{
    public class DatabaseContextFactory : OpenModMySqlDbContextFactory<DatabaseContext>
    {
    }
}
