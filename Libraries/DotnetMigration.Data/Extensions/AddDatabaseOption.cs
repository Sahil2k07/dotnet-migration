using DotnetMigration.Data.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetMigration.Data.Extensions;

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
                            configuration["Database:DbServer"]
                            ?? configuration["DB_SERVER"]
                            ?? throw new InvalidOperationException("Database server is required.");

                        options.Database =
                            configuration["Database:DbDatabase"]
                            ?? configuration["DB_DATABASE"]
                            ?? throw new InvalidOperationException(
                                "Database database is required."
                            );

                        options.Username =
                            configuration["Database:DbUsername"]
                            ?? configuration["DB_USERNAME"]
                            ?? throw new InvalidOperationException(
                                "Database username is required."
                            );

                        options.Password =
                            configuration["Database:DbPassword"]
                            ?? configuration["DB_PASSWORD"]
                            ?? throw new InvalidOperationException(
                                "Database password is required."
                            );
                    }
                );

            return services;
        }
    }
}
