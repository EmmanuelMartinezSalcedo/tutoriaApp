namespace tutoriaBE.Core.UserAggregate.Policies;
public static class IdPolicy
{
  public static bool IsNumber(int? id)
  {
    return id.HasValue;
  }

  public static List<string> Validate(int? id)
  {
    var errors = new List<string>();

    if (!IsNumber(id))
      errors.Add("Id must be a number.");

    return errors;
  }
}
