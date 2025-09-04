namespace tutoriaBE.Core.UserAggregate.Policies;

public static class FirstNamePolicy
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
      errors.Add($"First name must be at least {MinimumLength} characters long.");

    return errors;
  }

  public static bool IsValid(string password)
  {
    return HasMinimumLength(password);
  }
}
