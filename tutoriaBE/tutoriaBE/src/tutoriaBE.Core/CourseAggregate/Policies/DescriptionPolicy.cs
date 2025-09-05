namespace tutoriaBE.Core.CourseAggregate.Policies;
public static class DescriptionPolicy
{
  public const int MinimumLength = 3;
  public const int MaximumLength = 1000;

  public static bool HasMinimumLength(string description)
  {
    return !string.IsNullOrEmpty(description) && description.Length >= MinimumLength;
  }
  public static bool HasMaximumLength(string description)
  {
    return !string.IsNullOrEmpty(description) && description.Length <= MaximumLength;
  }

  public static List<string> Validate(string description)
  {
    var errors = new List<string>();

    if (!HasMinimumLength(description))
      errors.Add($"Description must be at least {MinimumLength} characters long.");

    if (!HasMaximumLength(description))
      errors.Add($"Description must be at most {MaximumLength} characters long.");
    return errors;
  }
}
