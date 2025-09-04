using System.Text.RegularExpressions;

public static class EmailPolicy
{
  private static readonly Regex EmailRegex =
    new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

  public static bool IsNotEmpty(string email)
  {
    return !string.IsNullOrWhiteSpace(email);
  }

  public static bool IsValidFormat(string email)
  {
    return !string.IsNullOrEmpty(email) && EmailRegex.IsMatch(email);
  }

  public static List<string> Validate(string email)
  {
    var errors = new List<string>();
    if (!IsNotEmpty(email))
      errors.Add("Email cannot be empty.");

    if (!IsValidFormat(email))
      errors.Add("Invalid email format.");
    return errors;
  }
}
