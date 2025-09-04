namespace tutoriaBE.Core.UserAggregate.Policies;

public static class LastNamePolicy
{
  public const int MinimumLength = 3;

  public static bool HasMinimumLength(string lastName)
  {
    return !string.IsNullOrEmpty(lastName) && lastName.Length >= MinimumLength;
  }

  public static List<string> Validate(string lastName)
  {
    var errors = new List<string>();

    if (!HasMinimumLength(lastName))
      errors.Add($"Last name must be at least {MinimumLength} characters long.");

    return errors;
  }

  public static bool IsValid(string password)
  {
    return HasMinimumLength(password);
  }
}
