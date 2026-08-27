using DotnetMigration.Data.Configuration;
using DotnetMigration.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetMigration.Data;

public static class DotnetMigrationData
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDotnetMigrationData()
        {
            #region Options

            services.AddDatabaseOptions();

            #endregion

            services.AddDbContext<DotnetMigrationContext>();

            return services;
        }
    }
}
