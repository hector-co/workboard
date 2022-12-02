using Microsoft.EntityFrameworkCore;

namespace Workboard.Infrastructure.DataAccess.EF;

public class WorkboardContext : DbContext
{
    public const string DbSchema = "workboard";

    public WorkboardContext(DbContextOptions<WorkboardContext> options) : base(options)
    {
    }
}

