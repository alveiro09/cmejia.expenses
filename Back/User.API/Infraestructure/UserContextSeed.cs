using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Polly;
using System;
using System.Linq;
using User.API.Application.Model;
using UserManagement.Domain.Infraestructure;

namespace User.API.Infraestructure
{

    /// <summary>
    /// Static class which checks migrations
    /// </summary>
    public static class UserContextSeed
    {
        /// <summary>
        /// checks if a migration can be applied
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                                     .GetAppliedMigrations()
                                     .Select(migration => migration.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                               .Migrations
                               .Select(migration => migration.Key);

            return !total.Except(applied).Any();
        }

        /// <summary>
        /// Get the seed to fill the databasse
        /// </summary>
        /// <param name="context"></param>
        /// <param name="settings"></param>
        /// <param name="environment"></param>
        /// <param name="logger"></param>
        public static void EnsureSeed(this UserManagementContext context, IOptions<UserSettings> settings, IHostingEnvironment environment, ILogger logger)
        {
            var seedPolicy = CreateSeedPolicy(logger, nameof(UserContextSeed));
            seedPolicy.Execute(() =>
            {
                var useSampleData = settings.Value.UseSampleData;
                context.SaveChangesAsync();
            });
        }

        private static Policy CreateSeedPolicy(ILogger logger, string prefix, short retries = 3)
        {
            return Policy.Handle<MySqlException>()
                .WaitAndRetry(
                        retryCount: retries,
                        sleepDurationProvider: retry => TimeSpan.FromSeconds(10),
                        onRetry: (exception, timeSpan, retry, ctx) => logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}")
                    );
        }
    }
}
