namespace tutoriaBE.Core.UserAggregate.Policies;
public class DayOfWeekPolicy
{
  public static bool IsValidDay(ScheduleSlotDayOfWeek day)
  {
    return ScheduleSlotDayOfWeek.List.Contains(day);
  }

  public static List<string> Validate(ScheduleSlotDayOfWeek day)
  {
    var errors = new List<string>();

    if (!IsValidDay(day))
      errors.Add($"Invalid day of the week: {day?.Name ?? "null"}.");

    return errors;
  }
}
