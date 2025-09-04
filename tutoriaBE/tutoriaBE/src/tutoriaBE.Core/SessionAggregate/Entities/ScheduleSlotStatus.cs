namespace tutoriaBE.Core.UserAggregate;

public class ScheduleSlotStatus : SmartEnum<ScheduleSlotStatus>
{
  public static readonly ScheduleSlotStatus Available = new(nameof(Available), 1);
  public static readonly ScheduleSlotStatus Booked = new(nameof(Booked), 2);
  protected ScheduleSlotStatus(string name, int value) : base(name, value) { }
}

