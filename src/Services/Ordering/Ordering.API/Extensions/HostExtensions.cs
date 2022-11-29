using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrationDatabase<TContext>(
            this IHost host,Action<TContext,IServiceProvider> seeder,
            int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context= services.GetService<TContext>();
                
                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContext}",typeof(TContext));

                    InvokeSeeder(seeder, context, services);

                    logger.LogInformation("Migrated database associated with context {DbContext}",typeof(TContext));
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occured while Migrating the database used on context");
                    if (retryForAvailability <50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrationDatabase<TContext>(host, seeder, retryForAvailability);
                    }
                }
                return host;
            }
        }

        private static void InvokeSeeder<TContext>(Action<TContext,IServiceProvider> seeder,TContext context ,IServiceProvider services)where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
