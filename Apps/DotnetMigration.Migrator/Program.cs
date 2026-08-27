using DotnetMigration.Data;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDotnetMigrationData();
