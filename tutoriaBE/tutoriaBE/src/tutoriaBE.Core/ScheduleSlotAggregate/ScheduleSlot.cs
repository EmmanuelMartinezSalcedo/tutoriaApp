namespace tutoriaBE.Core.ScheduleSlotAggregate;

public enum DayOfWeekSlot
{
  Monday,
  Tuesday,
  Wednesday,
  Thursday,
  Friday,
  Saturday,
  Sunday
}

public enum SlotStatus
{
  Available,
  Booked
}

public class ScheduleSlot : EntityBase, IAggregateRoot
{
  public int TutorId { get; private set; }
  public DayOfWeekSlot DayOfWeek { get; private set; }
  public TimeSpan HourStart { get; private set; }
  public SlotStatus Status { get; private set; } = SlotStatus.Available;

  // -----------------------------
  // Navigation properties
  // -----------------------------

  

  // -----------------------------
  // Constructors
  // -----------------------------

  private ScheduleSlot() { } // EF Core

  public ScheduleSlot(int tutorId, DayOfWeekSlot dayOfWeek, TimeSpan hourStart)
  {
    UpdateTutorId(tutorId);
    UpdateDayOfWeek(dayOfWeek);
    UpdateHourStart(hourStart);
    UpdateStatus(SlotStatus.Available);
  }

  // -----------------------------
  // Update methods
  // -----------------------------

  public ScheduleSlot UpdateTutorId(int tutorId)
  {
    TutorId = Guard.Against.NegativeOrZero(tutorId, nameof(tutorId));
    return this;
  }

  public ScheduleSlot UpdateDayOfWeek(DayOfWeekSlot dayOfWeek)
  {
    DayOfWeek = Guard.Against.EnumOutOfRange(dayOfWeek, nameof(dayOfWeek));
    return this;
  }

  public ScheduleSlot UpdateHourStart(TimeSpan hourStart)
  {
    HourStart = Guard.Against.Default(hourStart, nameof(hourStart));
    return this;
  }

  public ScheduleSlot UpdateStatus(SlotStatus status)
  {
    Status = Guard.Against.EnumOutOfRange(status, nameof(status));
    return this;
  }
}
