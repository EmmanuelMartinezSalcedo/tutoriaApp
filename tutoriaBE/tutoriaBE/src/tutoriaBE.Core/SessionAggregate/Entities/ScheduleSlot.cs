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

  public virtual List<SessionSlot>? SessionSlots { get; private set; }

  // -----------------------------
  // Constructors
  // -----------------------------

  protected ScheduleSlot() { } // EF Core

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
    if (!ScheduleSlotDayOfWeek.List.Contains(dayOfWeek))
      throw new ArgumentException("Invalid day of week", nameof(dayOfWeek));
  }


  private static void ValidateHour(int hour)
  {
    Guard.Against.OutOfRange(hour, nameof(hour), 0, 23);
  }

  public void book()
  {
    if (Status != ScheduleSlotStatus.Available)
      throw new InvalidOperationException("Only available slots can be booked.");
    Status = ScheduleSlotStatus.Booked;
  }

  // -----------------------------
  // Update methods
  // -----------------------------
}
