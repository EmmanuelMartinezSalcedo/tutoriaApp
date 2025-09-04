namespace tutoriaBE.Core.UserAggregate;

public class ScheduleSlotDayOfWeek : SmartEnum<ScheduleSlotDayOfWeek>
{
  public static readonly ScheduleSlotDayOfWeek Monday = new(nameof(Monday), 1);
  public static readonly ScheduleSlotDayOfWeek Tuesday = new(nameof(Tuesday), 2);
  public static readonly ScheduleSlotDayOfWeek Wednesday = new(nameof(Wednesday), 3);
  public static readonly ScheduleSlotDayOfWeek Thursday = new(nameof(Thursday), 4);
  public static readonly ScheduleSlotDayOfWeek Friday = new(nameof(Friday), 5);
  public static readonly ScheduleSlotDayOfWeek Saturday = new(nameof(Saturday), 6);
  public static readonly ScheduleSlotDayOfWeek Sunday = new(nameof(Sunday), 7);
  protected ScheduleSlotDayOfWeek(string name, int value) : base(name, value) { }
}

