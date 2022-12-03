using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;
using Workboard.Domain.Model;

namespace Workboard.Infrastructure.DataAccess.EF.Cards;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    private readonly string _dbSchema;

    public CardConfiguration(string dbSchema)
    {
        _dbSchema = dbSchema;
    }

    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Card", _dbSchema);
        builder.Property(m => m.Name)
            .HasMaxLength(100);
        builder.Property(m => m.EstimatedPoints)
            .HasColumnType("decimal(3, 2)");
        builder.HasMany(m => m.Owners)
            .WithMany(r => r.CardOwners)
            .UsingEntity<Dictionary<string, object>>(
                "CardOwner",
                j => j
                    .HasOne<Developer>()
                    .WithMany()
                    .HasForeignKey("OwnerId"),
                j => j
                    .HasOne<Card>()
                    .WithMany()
                    .HasForeignKey("CardId"));
    }
}