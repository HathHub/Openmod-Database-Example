using System;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OpenMod.API.Plugins;
using OpenMod.EntityFrameworkCore.MySql.Extensions;
using OpenMod.Unturned.Plugins;
using OpenmodDatabaseExample.Databases;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

namespace OpenmodDatabaseExample
{
    public class PluginContainerConfigurator : IPluginContainerConfigurator
    {
        public void ConfigureContainer(IPluginServiceConfigurationContext context)
        {
            // You can extend how your database context works by using the overloads of this method.
            context.ContainerBuilder.AddMySqlDbContext<DatabaseContext>();
        }
    }
}
