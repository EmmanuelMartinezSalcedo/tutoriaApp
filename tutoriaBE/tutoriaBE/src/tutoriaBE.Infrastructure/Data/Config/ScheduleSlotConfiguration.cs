using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tutoriaBE.Core.SessionAggregate;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.UserAggregate.Entities;

namespace tutoriaBE.Infrastructure.Data.Config;

public class ScheduleSlotConfiguration : IEntityTypeConfiguration<ScheduleSlot>
{
  public void Configure(EntityTypeBuilder<ScheduleSlot> builder)
  {
    builder.HasOne<Tutor>()
      .WithMany(t => t.ScheduleSlots)
      .HasForeignKey(s => s.TutorId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.Property(s => s.DayOfWeek)
      .HasConversion(
        v => v.Name,
        v => ScheduleSlotDayOfWeek.FromName(v, true)
      )
      .HasMaxLength(20)
      .IsRequired();

    builder.Property(s => s.StartHour)
      .IsRequired();

    builder.Property(s => s.Status)
     .HasConversion(
       v => v.Name,
       v => ScheduleSlotStatus.FromName(v, true)
     )
     .HasMaxLength(20)
     .IsRequired();
  }
}
