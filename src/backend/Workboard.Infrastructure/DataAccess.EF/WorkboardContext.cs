using Microsoft.EntityFrameworkCore;
using Workboard.Infrastructure.DataAccess.EF.Developers;
using Workboard.Infrastructure.DataAccess.EF.Cards;
using Workboard.Infrastructure.DataAccess.EF.Boards;

namespace Workboard.Infrastructure.DataAccess.EF;

public class WorkboardContext : DbContext
{
    public const string DbSchema = "workboard";

    public WorkboardContext(DbContextOptions<WorkboardContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Configure(modelBuilder);
    }

    public static void Configure(ModelBuilder modelBuilder, string dbSchema = DbSchema)
    {
        modelBuilder.ApplyConfiguration(new DeveloperConfiguration(dbSchema));
        modelBuilder.ApplyConfiguration(new CardConfiguration(dbSchema));
        modelBuilder.ApplyConfiguration(new BoardConfiguration(dbSchema));
        modelBuilder.ApplyConfiguration(new ColumnConfiguration(dbSchema));
    }
}