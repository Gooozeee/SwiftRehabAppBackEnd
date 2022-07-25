using Npgsql;

namespace SwiftUserManagement.API.Extensions
{
    public static class HostExtensions
    {
        // Migrates the database and inserts dummy data into the new database
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            if (retry == null)
            {
                retry = 0;
            }

            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating PostgreSQL Database");

                    using var connection = new NpgsqlConnection
                        (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Users";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Users(Id SERIAL PRIMARY KEY,
                                                                Email VARCHAR(24) NOT NULL,
                                                                UserName VARCHAR(24) NOT NULL,
                                                                Password VARCHAR(24) NOT NULL,
                                                                Role VARCHAR(24) NOT NULL,
                                                                )";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Users(Email, UserName, Password, Role) VALUES('michalguzym@gmail.com', 'Michal Guzy', 'password', 'User');";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Users(Email, UserName, Password, Role) VALUES('sophie@gmail.com', 'Sophie Young', 'password', 'User');";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated postgresql database.");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occured while migrating the PostgreSQL database");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }
            }

            return host;
        }
    }
}
