using DotnetMigration.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DotnetMigration.Data.Context;

public sealed class DotnetMigrationContext : DbContext
{
    private readonly DatabaseOptions _dbOptions;

    public DotnetMigrationContext(
        DbContextOptions<DotnetMigrationContext> options,
        IOptions<DatabaseOptions> dbOptions
    )
        : base(options)
    {
        _dbOptions = dbOptions.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (!optionsBuilder.IsConfigured)
        {
            var connectionString =
                $"Server={_dbOptions.Server};"
                + $"Database={_dbOptions.Database};"
                + $"User Id={_dbOptions.Username};"
                + $"Password={_dbOptions.Password};"
                + $"TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
