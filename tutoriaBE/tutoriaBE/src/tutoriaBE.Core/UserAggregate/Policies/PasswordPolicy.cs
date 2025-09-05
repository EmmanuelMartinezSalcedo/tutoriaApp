namespace tutoriaBE.Core.UserAggregate.Policies;

public static class PasswordPolicy
{
  public const int MinimumLength = 8;
  private static readonly char[] AllowedSymbols = "!@#$%^&*()_+-=[]{}|;:,.<>/?".ToCharArray();

  public static bool HasMinimumLength(string password)
  {
    return !string.IsNullOrEmpty(password) && password.Length >= MinimumLength;
  }

  public static bool HasUppercase(string password)
  {
    return !string.IsNullOrEmpty(password) && password.Any(char.IsUpper);
  }

  public static bool HasLowercase(string password)
  {
    return !string.IsNullOrEmpty(password) && password.Any(char.IsLower);
  }

  public static bool HasDigit(string password)
  {
    return !string.IsNullOrEmpty(password) && password.Any(char.IsDigit);
  }

  public static bool HasSymbol(string password)
  {
    return !string.IsNullOrEmpty(password) && password.Any(c => AllowedSymbols.Contains(c));
  }

  public static List<string> Validate(string password)
  {
    var errors = new List<string>();

    if (!HasMinimumLength(password))
      errors.Add($"Password must be at least {MinimumLength} characters long.");

    if (!HasUppercase(password))
      errors.Add("Password must contain at least one uppercase letter.");

    if (!HasLowercase(password))
      errors.Add("Password must contain at least one lowercase letter.");

    if (!HasDigit(password))
      errors.Add("Password must contain at least one number.");

    if (!HasSymbol(password))
      errors.Add("Password must contain at least one symbol.");

    return errors;
  }
}
