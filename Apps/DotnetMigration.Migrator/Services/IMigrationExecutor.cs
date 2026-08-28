using DotnetMigration.Migrator.Helpers;

namespace DotnetMigration.Migrator.Services;

public interface IMigrationExecutor
{
    Task ApplyMigration(IReadOnlyList<MigrationFile> migrationFiles);
}
