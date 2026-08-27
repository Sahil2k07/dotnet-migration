using DotnetMigration.Data;
using DotnetMigration.Migrator.Scripts;
using DotnetMigration.Migrator.Services;
using DotnetMigration.Migrator.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDotnetMigrationData();

builder.Services.AddScoped<IMigrationHistoryService, MigrationHistoryService>();
builder.Services.AddScoped<IMigrationRunner, MigrationRunner>();

var app = builder.Build();

var runner = app.Services.GetRequiredService<IMigrationRunner>();

await runner.RunMigrationAsync();
