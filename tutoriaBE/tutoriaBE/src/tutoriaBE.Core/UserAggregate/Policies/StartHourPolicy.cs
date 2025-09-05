using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tutoriaBE.Core.UserAggregate.Policies;
public class StartHourPolicy
{
  public const int MinimumHour = 0;
  public const int MaximumHour = 23;

  public static bool HasMinimumHour(int? startHour)
  {
    return startHour.HasValue && startHour >= MinimumHour;
  }
  public static bool HasMaximumHour(int? startHour)
  {
    return startHour.HasValue && startHour <= MaximumHour;
  }

  public static List<string> Validate(int? startHour)
  {
    var errors = new List<string>();

    if (!HasMinimumHour(startHour))
      errors.Add($"Start Hour must be at least {MinimumHour}.");

    if (!HasMaximumHour(startHour))
      errors.Add($"Start Hour must be at most {MaximumHour}.");

    return errors;
  }
}
