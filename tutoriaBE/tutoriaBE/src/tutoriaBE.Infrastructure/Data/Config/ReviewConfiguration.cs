using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tutoriaBE.Core.SessionAggregate;

namespace tutoriaBE.Infrastructure.Data.Config;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
  public void Configure(EntityTypeBuilder<Review> builder)
  {
    builder.HasOne<Session>()
      .WithOne(s => s.Review)
      .HasForeignKey<Review>(r => r.SessionId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.Property(r => r.Rating)
      .IsRequired()
      .HasPrecision(2, 1);

    builder.Property(r => r.Comment)
      .IsRequired()
      .HasMaxLength(2000);

    builder.Property(r => r.CreatedAt)
      .HasDefaultValueSql("CURRENT_TIMESTAMP");
  }
}
