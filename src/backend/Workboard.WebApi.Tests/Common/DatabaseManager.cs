using Npgsql;
using Respawn;
using Respawn.Graph;

namespace Workboard.WebApi.Tests.Common;

public class DatabaseManager
{
    public DatabaseManager(string connectionString)
    {
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        var respawner = Respawner.CreateAsync(conn, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            TablesToIgnore = new Table[]
            {
                        "__EFMigrationsHistory"
            },
            SchemasToInclude = new[] { "workboard" },
        }).Result;

        respawner.ResetAsync(conn).Wait();
    }
}
