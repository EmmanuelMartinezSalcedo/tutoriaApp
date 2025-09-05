using System;
using System.Collections.Generic;

namespace tutoriaBE.Core.UserAggregate.Policies;

public static class StartEndHourPolicy
{
  public static bool IsStartBeforeEnd(int startHour, int endHour)
  {
    return startHour < endHour;
  }

  public static List<string> Validate(int startHour, int endHour)
  {
    var errors = new List<string>();

    if (!IsStartBeforeEnd(startHour, endHour))
      errors.Add("Start hour must be less than end hour.");

    return errors;
  }
}
