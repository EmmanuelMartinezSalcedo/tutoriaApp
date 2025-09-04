using tutoriaBE.Core.SessionAggregate;

namespace tutoriaBE.Infrastructure.Data.Config;

public class SessionSlotConfiguration : IEntityTypeConfiguration<SessionSlot>
{
  public void Configure(EntityTypeBuilder<SessionSlot> builder)
  {
    builder.HasKey(ss => new { ss.SessionId, ss.ScheduleSlotId });

    builder.Ignore(ss => ss.Id);

    builder.HasOne<Session>()
      .WithMany(s => s.SessionSlots)
      .HasForeignKey(ss => ss.SessionId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne<ScheduleSlot>()
      .WithMany(ss => ss.SessionSlots)
      .HasForeignKey(ss => ss.ScheduleSlotId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
