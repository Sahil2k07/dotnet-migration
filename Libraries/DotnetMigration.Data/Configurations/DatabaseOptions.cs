namespace DotnetMigration.Data.Configurations;

public sealed class DatabaseOptions
{
    public required string Server { get; set; }

    public required string Database { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }
}
