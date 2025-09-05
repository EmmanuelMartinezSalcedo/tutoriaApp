namespace tutoriaBE.Core.CourseAggregate.Policies;
public static class TitlePolicy
{
  public const int MinimumLength = 3;

  public static bool HasMinimumLength(string firstName)
  {
    return !string.IsNullOrEmpty(firstName) && firstName.Length >= MinimumLength;
  }

  public static List<string> Validate(string firstName)
  {
    var errors = new List<string>();

    if (!HasMinimumLength(firstName))
      errors.Add($"Title must be at least {MinimumLength} characters long.");

    return errors;
  }
}
