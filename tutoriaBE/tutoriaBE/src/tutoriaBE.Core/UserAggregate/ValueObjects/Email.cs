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
    var errors = EmailPolicy.Validate(value);
    if (errors.Any())
    {
      var validationErrors = errors
        .Select(error => new ValidationError
        {
          Identifier = nameof(Email),
          ErrorMessage = error
        }).ToList();

      return Result.Invalid(validationErrors);
    }

    return Result.Success(new Email(value));
  }


  public override bool Equals(object? obj) => Equals(obj as Email);

  public bool Equals(Email? other) => other is not null && Value == other.Value;

  public override int GetHashCode() => Value.GetHashCode();

  public override string ToString() => Value;

  public static implicit operator string(Email email) => email.Value;
}
