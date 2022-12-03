using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;
using Workboard.Domain.Model;

namespace Workboard.Infrastructure.DataAccess.EF.Developers;

public class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
{
    private readonly string _dbSchema;

    public DeveloperConfiguration(string dbSchema)
    {
        _dbSchema = dbSchema;
    }

    public void Configure(EntityTypeBuilder<Developer> builder)
    {
        builder.ToTable("Developer", _dbSchema);
        builder.Property(m => m.Name)
            .HasMaxLength(100);
    }
}