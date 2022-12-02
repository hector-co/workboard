using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Workboard.Infrastructure.DataAccess.EF;

internal class WorkboardContextFactory : IDesignTimeDbContextFactory<WorkboardContext>
{
    public WorkboardContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WorkboardContext>();
        optionsBuilder.UseNpgsql(
            "Host=localhost;Database=Workboard;Username=postgres;Password=postgres",
            o => o.MigrationsHistoryTable("__EFMigrationsHistory", WorkboardContext.DbSchema));

        return new WorkboardContext(optionsBuilder.Options);
    }
}