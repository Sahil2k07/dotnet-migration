using System.Diagnostics;
using DotnetMigration.Migrator.Helpers;
using DotnetMigration.Migrator.Services;

namespace DotnetMigration.Migrator.Scripts;

public sealed class MigrationRunner : IMigrationRunner
{
    private readonly IMigrationHistoryService _historyService;

    public MigrationRunner(IMigrationHistoryService historyService)
    {
        _historyService = historyService;
    }

    public async Task RunMigrationAsync()
    {
        var stopWatch = new Stopwatch();

        IReadOnlyList<MigrationHistory> migrationHistories =
            await _historyService.GetMigrationHistories();
    }
}
