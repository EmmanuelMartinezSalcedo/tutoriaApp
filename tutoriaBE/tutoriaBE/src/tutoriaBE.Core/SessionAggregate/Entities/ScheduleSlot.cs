using tutoriaBE.Core.UserAggregate;

namespace tutoriaBE.Core.SessionAggregate;

public class ScheduleSlot : EntityBase
{
  public int TutorId { get; private set; }
  public ScheduleSlotDayOfWeek DayOfWeek { get; private set; } = default!;
  public int StartHour { get; private set; } = default!;
  public ScheduleSlotStatus Status { get; private set; } = default!;

  // -----------------------------
  // Navigation properties
  // -----------------------------

  public List<SessionSlot>? SessionSlots { get; private set; }

  // -----------------------------
  // Constructors
  // -----------------------------

  private ScheduleSlot() { } // EF Core

  public ScheduleSlot(int tutorId, ScheduleSlotDayOfWeek dayOfWeek, int startHour)
  {
    TutorId = Guard.Against.NegativeOrZero(tutorId, nameof(tutorId));
    ValidateDayOfWeek(dayOfWeek);
    ValidateHour(startHour);
    DayOfWeek = dayOfWeek;
    StartHour = startHour;
    Status = ScheduleSlotStatus.Available;
  }

  // -----------------------------
  // Validation methods
  // -----------------------------

  private static void ValidateDayOfWeek(ScheduleSlotDayOfWeek dayOfWeek)
  {
    if (!Enum.IsDefined(typeof(ScheduleSlotDayOfWeek), dayOfWeek))
      throw new ArgumentException("Invalid day of week", nameof(dayOfWeek));
  }

  private static void ValidateHour(int hour)
  {
    Guard.Against.OutOfRange(hour, nameof(hour), 0, 23);
  }

  // -----------------------------
  // Update methods
  // -----------------------------
}
