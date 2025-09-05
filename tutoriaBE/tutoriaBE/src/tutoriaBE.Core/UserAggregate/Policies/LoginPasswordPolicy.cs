namespace tutoriaBE.Core.UserAggregate.Policies;
public static class LoginPasswordPolicy
{
  public const int MinimumLength = 8;

  public static bool HasMinimumLength(string password)
  {
    return !string.IsNullOrWhiteSpace(password) && password.Length >= MinimumLength;
  }


  public static List<string> Validate(string password)
  {
    var errors = new List<string>();

    if (!HasMinimumLength(password))
      errors.Add($"Password must be at least {MinimumLength} characters long.");

    return errors;
  }
}
