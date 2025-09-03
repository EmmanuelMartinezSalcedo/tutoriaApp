namespace tutoriaBE.Core.UserAggregate.ValueObjects;

public class Email : IEquatable<Email>
{
  public string Value { get; }

  private Email(string value)
  {
    Value = value;
  }

  public static Result<Email> Create(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(Email),
        ErrorMessage = "Email cannot be empty"
      });
    }

    if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(Email),
        ErrorMessage = "Invalid email format"
      });
    }

    return Result.Success(new Email(value));
  }

  public override bool Equals(object? obj) => Equals(obj as Email);

  public bool Equals(Email? other) => other is not null && Value == other.Value;

  public override int GetHashCode() => Value.GetHashCode();

  public override string ToString() => Value;

  public static implicit operator string(Email email) => email.Value;
}
