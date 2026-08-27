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
                        options.Server =
                            configuration["DB_SERVER"]
                            ?? throw new InvalidOperationException("DB_SERVER is required.");

                        options.Database =
                            configuration["DB_DATABASE"]
                            ?? throw new InvalidOperationException("DB_DATABASE is required.");

                        options.Username =
                            configuration["DB_USERNAME"]
                            ?? throw new InvalidOperationException("DB_USERNAME is required.");

                        options.Password =
                            configuration["DB_PASSWORD"]
                            ?? throw new InvalidOperationException("DB_PASSWORD is required.");
                    }
                );
            return services;
        }
    }
}
