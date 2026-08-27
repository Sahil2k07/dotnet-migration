using DotnetMigration.Migrator.Helpers;

namespace DotnetMigration.Migrator.Services;

public interface IMigrationHistoryService
{
    Task<IReadOnlyList<MigrationHistory>> GetMigrationHistories();

    Task SaveMigrationHistory(ICollection<MigrationHistory> migrationHistories);
}
