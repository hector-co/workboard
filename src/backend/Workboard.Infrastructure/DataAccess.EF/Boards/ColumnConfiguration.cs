using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;
using Workboard.Domain.Model;

namespace Workboard.Infrastructure.DataAccess.EF.Boards;

public class ColumnConfiguration : IEntityTypeConfiguration<Column>
{
    private readonly string _dbSchema;

    public ColumnConfiguration(string dbSchema)
    {
        _dbSchema = dbSchema;
    }

    public void Configure(EntityTypeBuilder<Column> builder)
    {
        builder.ToTable("Column", _dbSchema);
        builder.Property(m => m.Name)
            .HasMaxLength(100);
    }
}