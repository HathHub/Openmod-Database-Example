# OpenMod Database Example Plugin

This is a simple example of using the OpenMod Entity Framework for database operations in an OpenMod Unturned plugin. The plugin demonstrates how to connect to a database, perform migrations, and execute basic CRUD operations.

## Getting Started

1. **Install OpenMod:** Make sure you have OpenMod installed in your Unturned server. If not, follow the [official OpenMod installation guide](https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html).

2. **Plugin Installation:**
    - Copy the compiled plugin DLL to the `Plugins` directory in your Unturned server.

3. **Configuration:**
    - No additional configuration is needed for this example.

4. **Database Setup:**
    - Ensure you have a database configured according to the connection string in your `appsettings.json` or equivalent configuration file.
  
# Config.yaml

## Default MySQL Connection

By default, OpenMod retrieves the connection string for your MySQL database from the `config.yaml` file. Ensure that the following configuration is present in your `config.yaml`:

```yaml
database:
  ConnectionStrings:
    default: "Server=127.0.0.1; Database=openmod; Port=3306; User=root; Password=toor;"
```

Adjust the values in the connection string according to your MySQL database configuration. This ensures that OpenMod can establish a connection to your MySQL database when the plugin is loaded.

For more details on OpenMod configuration, refer to the [official documentation](https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html#configuring-openmod).


## Usage Example

```csharp
protected override async UniTask OnLoadAsync()
{
    // Uncomment the line below if you have to access Unturned or UnityEngine APIs
    // await UniTask.SwitchToMainThread();

    // Log a greeting message
    m_Logger.LogInformation("Hello World!");

    // Get the DatabaseContext from the service provider
    await using var dbContext = m_ServiceProvider.GetRequiredService<DatabaseContext>();

    // Perform database migrations
    await dbContext.Database.MigrateAsync();

    // Create a new player entry
    Players player = new Players
    {
        SteamID = 12332
    };

    // Uncomment the line below to add the player to the database
    // await dbContext.Players.AddAsync(player);

    // Save changes to the database
    await dbContext.SaveChangesAsync();

    // Retrieve a player by SteamID
    Players? retrievedPlayer = await dbContext.Players
        .Where(x => x.SteamID == 12332)
        .FirstOrDefaultAsync();

    // Check if the player was found
    if (retrievedPlayer is null)
    {
        m_Logger.LogInformation("Couldn't find the user.");
    }
    else
    {
        // Log the player information
        m_Logger.LogInformation(retrievedPlayer.ToJson().ToString());
    }
}
```

# Notes

- The `UniTask.SwitchToMainThread()` line is commented out. Uncomment it if you need to access Unturned or UnityEngine APIs within the `OnLoadAsync` method.

- Ensure your database is properly configured and accessible.

- The example includes basic database operations such as migrations, adding a player, and retrieving a player by SteamID.

- Customize this example to fit the specific needs of your plugin.

- For more information, refer to the [official OpenMod documentation](https://openmod.github.io/openmod-docs/devdoc/concepts/databases.html).
