using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tutoriaBE.Core.UserAggregate.Entities;

namespace tutoriaBE.Infrastructure.Data.Config;

public class TutorConfiguration : IEntityTypeConfiguration<Tutor>
{
  public void Configure(EntityTypeBuilder<Tutor> builder)
  {
    builder.Property(t => t.Id)
      .ValueGeneratedNever();

    builder.Property(t => t.Bio)
      .HasMaxLength(1000);

    builder.HasMany(t => t.TutorCourses)
      .WithOne()
      .HasForeignKey(tc => tc.TutorId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(t => t.ScheduleSlots)
      .WithOne()
      .HasForeignKey(s => s.TutorId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
