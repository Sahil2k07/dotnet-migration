using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetMigration.Data.Configuration;

public sealed class DatabaseOptions
{
    public required string Server { get; set; }

    public required string Database { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }
}

public static class DatabaseExtension
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDatabaseOptions()
        {
            services
                .AddOptions<DatabaseOptions>()
                .Configure<IConfiguration>(
                    (options, configuration) =>
                    {
                        options.Server = configuration["DB_SERVER"] ?? string.Empty;
                        options.Database = configuration["DB_DATABASE"] ?? string.Empty;
                        options.Username = configuration["DB_USERNAME"] ?? string.Empty;
                        options.Password = configuration["DB_PASSWORD"] ?? string.Empty;
                    }
                );
            return services;
        }
    }
}
