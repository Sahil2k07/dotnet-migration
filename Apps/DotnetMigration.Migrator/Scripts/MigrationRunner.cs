using System.Diagnostics;
using DotnetMigration.Data.SQL;
using DotnetMigration.Migrator.Helpers;
using DotnetMigration.Migrator.Services;
using Microsoft.Extensions.Logging;

namespace DotnetMigration.Migrator.Scripts;

public sealed class MigrationRunner : IMigrationRunner
{
    private readonly ISQLExecutor _sqlExecutor;

    private readonly IMigrationFileService _fileService;

    private readonly ILogger<MigrationRunner> _logger;

    public MigrationRunner(
        ISQLExecutor sqlExecutor,
        IMigrationFileService fileService,
        ILogger<MigrationRunner> logger
    )
    {
        _sqlExecutor = sqlExecutor;
        _fileService = fileService;
        _logger = logger;
    }

    public async Task RunMigrationAsync()
    {
        var stopWatch = new Stopwatch();

        IReadOnlyList<MigrationFile> tables = await _fileService.GetMigrationFiles(
            "Migrations/Tables",
            "TABLE",
            true
        );

        IReadOnlyList<MigrationFile> indexes = await _fileService.GetMigrationFiles(
            "Migrations/Indexes",
            "INDEX",
            true
        );

        IReadOnlyList<MigrationFile> views = await _fileService.GetMigrationFiles(
            "Migrations/Views",
            "VIEW",
            false
        );

        IReadOnlyList<MigrationFile> functions = await _fileService.GetMigrationFiles(
            "Migrations/Functions",
            "FUNCTION",
            false
        );

        IReadOnlyList<MigrationFile> procedures = await _fileService.GetMigrationFiles(
            "Migrations/Procedures",
            "PROCEDURE",
            false
        );

        IReadOnlyList<MigrationFile> triggers = await _fileService.GetMigrationFiles(
            "Migrations/Triggers",
            "TRIGGER",
            false
        );

        _logger.LogInformation(
            "Migration SQL scripts read. Pending migrations: {Count}",
            tables.Count
                + indexes.Count
                + views.Count
                + functions.Count
                + procedures.Count
                + triggers.Count
        );
    }
}
