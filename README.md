# .NET Migrations

A SQL file-based migration runner for .NET applications, built around a simple idea: **keep your migration scripts in Git while maintaining migration history and change detection in the database.**

I built this project after working on a data-driven .NET application that relied heavily on database objects such as tables, indexes, functions, procedures, views, and triggers. That project used `RoundhousE` for database migrations, and I liked its approach of treating database objects such as procedures and functions as versioned objects rather than requiring a new migration history file for every change.

I wanted a migration system that could work naturally with an application's existing configuration and dependency injection infrastructure while supporting different behavior for different types of database objects.

In particular, I wanted to:

- Use the application's existing configuration instead of configuring database credentials separately for the migration tool.
- Organize migrations into separate folders such as `Tables`, `Indexes`, `Views`, `Functions`, `Procedures`, and `Triggers`.
- Treat tables and indexes differently from objects such as views, functions, procedures, and triggers.
- Avoid creating a new migration history file every time a function or procedure is modified.
- Detect when an already-applied migration has been modified.
- Execute migrations inside a database transaction.
- Use the standard .NET dependency injection and logging infrastructure.
- Keep the migration runner simple and configurable so it can be adapted to different applications.

## How Migrations Work

Migration files are organized under configurable migration folders:

    Migrations/
    ├── Tables/
    │   └── 2026/
    │       ├── 0001_CreateUser.sql
    │       ├── 0002_CreateUserProfile.sql
    │       └── 0003_CreateUserSession.sql
    │
    ├── Indexes/
    │   └── 2026/
    │       ├── 0001_IxUserIsActive.sql
    │       └── 0002_IxUserProfileLastName.sql
    │
    ├── Views/
    │   └── 0001_vw_UserDetails.sql
    │
    ├── Functions/
    │   └── 0001_fn_GetUserFullName.sql
    │
    ├── Procedures/
    │   └── 0001_sp_CreateUser.sql
    │
    └── Triggers/
        └── 0001_tr_UserUpdatedAt.sql

The folder structure and migration behavior can be changed to match the requirements of your application.

### Migration History

Every applied migration is recorded in the database along with a hash of the migration file.

The migration history stores information such as:

- File name
- File path
- File type
- File hash
- Created date
- Applied date

The file path identifies the migration, while the hash is used to determine whether the migration file has been modified after it was applied.

## Change Detection

Before applying migrations, the runner compares the current migration file with its corresponding entry in the migration history.

If the migration has already been applied and the file hash has not changed, the migration is skipped.

If the migration has already been applied but the file hash has changed, the behavior depends on the `throwOnChange` setting for that migration type.

The migration runner allows you to configure this behavior independently for each migration folder:

```csharp
IReadOnlyList<MigrationFile> tables = await _fileService.GetMigrationFiles(
   rootPath: "Migrations/Tables",
   fileType: "TABLE", // Stored as FileType in migration history
   throwOnChange: true
);

IReadOnlyList<MigrationFile> indexes = await _fileService.GetMigrationFiles(
   rootPath: "Migrations/Indexes",
   fileType: "INDEX",
   throwOnChange: true
);

IReadOnlyList<MigrationFile> views = await _fileService.GetMigrationFiles(
   rootPath: "Migrations/Views",
   fileType: "VIEW",
   throwOnChange: false
);
```

When `throwOnChange` is `true`, the migration runner throws an error if an already-applied migration file has been modified. This is useful for migrations such as **tables and indexes**, where you generally want an applied migration to remain immutable and changes to be introduced through a new migration.

When `throwOnChange` is `false`, an already-applied migration can be executed again when its contents have changed. This is useful for database objects such as **views, functions, procedures, and triggers**, where the SQL definition may naturally change over time.

If you use `throwOnChange: false`, make sure the SQL script is written so that it can be safely executed more than once. For example, the script should use an appropriate `CREATE OR ALTER` or other idempotent SQL pattern where supported.

### Migration Folders

Migration folders are completely configurable.

A folder mentioned in the configuration does **not** have to exist. If the folder is missing, the migration runner logs a warning and skips it. This makes it possible to configure folders that you may want to use in the future without requiring every folder to exist in every project.

You can also use your own migration folder structure. The only requirement is that the migration files are located somewhere under the `Migrations` directory.

For example, instead of using the default folder structure:

    Migrations/
    ├── Tables/
    ├── Indexes/
    ├── Views/
    ├── Functions/
    ├── Procedures/
    └── Triggers/

You could organize them by year:

    Migrations/
    ├── Tables/
    │   ├── 2026/
    │   └── 2027/
    ├── Indexes/
    │   ├── 2026/
    │   └── 2027/
    └── Procedures/
        ├── 2026/
        └── 2027/

This makes the migration runner flexible enough to support different organizational approaches without requiring changes to the migration engine itself.

## Configuration

The migration runner uses the standard .NET configuration system.

Database configuration can be provided through `appsettings.json`, environment variables, or any other configuration provider supported by .NET.

For example, the application can provide database settings through environment variables such as:

    DB_SERVER
    DB_DATABASE
    DB_USERNAME
    DB_PASSWORD

The migration runner does not require database credentials to be duplicated in the migration project.

This makes it possible to use the same migration runner across development, testing, staging, and production environments while keeping environment-specific configuration outside of source control.

## Dependency Injection

The project uses the standard .NET dependency injection infrastructure.

Services such as the database context, SQL executor, migration history service, migration file service, and migration executor can be registered with `IServiceCollection`.

The migrator itself is hosted as a .NET application and can use the standard .NET hosting infrastructure for configuration, dependency injection, logging, and application lifetime management.

## Transaction Support

Migrations are executed inside a database transaction.

The migration runner applies the pending migrations and commits the transaction only when the migration process completes successfully.

If a migration fails, the transaction is rolled back and the error is propagated to the application.

This helps prevent the database from being left in a partially migrated state.

## SQL Server

This project currently targets SQL Server and uses Entity Framework Core for database connectivity and transaction management while allowing migration SQL to be executed directly against the database.

The migration runner is intentionally SQL-first. Migration files contain the SQL that is ultimately executed against the database rather than relying on ORM-generated migrations.

## Why Build This?

There are already excellent migration tools available for .NET.

This project is intentionally not trying to replace mature general-purpose migration frameworks.

The goal is to provide a simple, customizable migration runner for applications that:

- Make heavy use of SQL Server database objects.
- Want migration scripts stored directly in Git.
- Want database objects to have a clear SQL source of truth.
- Need change detection for already-applied migrations.
- Want different behavior for different types of database objects.
- Want to integrate migrations with the application's existing .NET configuration and dependency injection infrastructure.

The project is intended to be used as a **custom migration runner/template** that can be adapted to the requirements of an individual application.

## Setup locally:

1. Clone the Repository:

   ```bash
   git clone https://github.com/Sahil2k07/dotnet-migration.git
   ```

2. Change Directory:

   ```bash
   cd dotnet-migration/Apps/DotnetMigration.Migrator
   ```

3. Configure your appsettings.json with SQL Server connection settings:

   ```json
   {
     "Logging": {
       "LogLevel": {
         "Microsoft.EntityFrameworkCore.Database.Command": "Information"
       }
     },
     "Database": {
       "Server": "localhost",
       "Database": "migration",
       "Username": "sa",
       "Password": "Shahil@12345"
     }
   }
   ```

4. Restore all the packages first:

   ```bash
   dotnet restore
   ```

5. Access to your local SQL-Server and create a database

   ```sql
   CREATE DATABASE migration;
   ```

6. Run migrations to create/update the database schema:

   ```bash
   dotnet run
   ```

7. Additionally you can make the Release build of the project using the command

   ```bash
   dotnet publish -c Release
   ```

- You can find the release build in the location `DotnetMigration.Migrator/bin/Release/net10.0/publish`
