using DotnetMigration.Migrator.Helpers;

namespace DotnetMigration.Migrator.Services;

public interface IMigrationFileService
{
    Task<IReadOnlyList<MigrationFile>> GetMigrationFiles(
        string rootPath,
        string fileType,
        bool throwOnChange = false
    );
}
