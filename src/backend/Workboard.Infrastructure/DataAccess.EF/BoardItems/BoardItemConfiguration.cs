using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;
using Workboard.Domain.Model;

namespace Workboard.Infrastructure.DataAccess.EF.BoardItems;

public class BoardItemConfiguration : IEntityTypeConfiguration<BoardItem>
{
    private readonly string _dbSchema;

    public BoardItemConfiguration(string dbSchema)
    {
        _dbSchema = dbSchema;
    }

    public void Configure(EntityTypeBuilder<BoardItem> builder)
    {
        builder.ToTable("BoardItem", _dbSchema);
        builder.HasOne(m => m.Board)
            .WithMany()
            .HasForeignKey(r => r.BoardId);
        builder.HasOne(m => m.Column)
            .WithMany()
            .HasForeignKey(r => r.ColumnId);
        builder.HasOne(m => m.Card)
            .WithMany()
            .HasForeignKey(r => r.CardId);
    }
}