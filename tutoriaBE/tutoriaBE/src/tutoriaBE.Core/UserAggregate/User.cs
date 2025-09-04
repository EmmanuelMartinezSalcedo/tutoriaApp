using tutoriaBE.Core.SessionAggregate;
using tutoriaBE.Core.UserAggregate.Entities;
using tutoriaBE.Core.UserAggregate.Policies;
using tutoriaBE.Core.UserAggregate.ValueObjects;

namespace tutoriaBE.Core.UserAggregate;

public class User : EntityBase, IAggregateRoot
{
  public string FirstName { get; private set; } = default!;
  public string LastName { get; private set; } = default!;
  public Email Email { get; private set; } = default!;
  public string PasswordHash { get; private set; } = default!;
  public string? ProfilePhotoPath { get; private set; }
  public UserRole Role { get; private set; } = UserRole.Student;
  public DateTime CreatedAt { get; private set; }

  // -----------------------------
  // Navigation properties
  // -----------------------------

  public Tutor? TutorProfile { get; private set; }
  public List<Message>? Messages { get; private set; }

  // -----------------------------
  // Constructors
  // -----------------------------

  private User() { } // EF Core

  public static Result<User> Create(string firstName, string lastName, string email, string password, IPasswordHasher hasher)
  {
    var validationErrors = new List<ValidationError>();
    var user = new User();

    // First Name
    var firstNameErrors = FirstNamePolicy.Validate(firstName);
    if (firstNameErrors.Any())
    {
      var firstNameValidationErrors = firstNameErrors
          .Select(error => new ValidationError
          {
            Identifier = nameof(firstName),
            ErrorMessage = error
          })
          .ToList();
      validationErrors.AddRange(firstNameValidationErrors);
    }
    else
    {
      user.FirstName = firstName;
    }

    // Last Name
    var lastNameErrors = LastNamePolicy.Validate(lastName);
    if (lastNameErrors.Any())
    {
      var lastNameValidationErrors = lastNameErrors
          .Select(error => new ValidationError
          {
            Identifier = nameof(lastName),
            ErrorMessage = error
          })
          .ToList();
      validationErrors.AddRange(lastNameValidationErrors);
    }
    else
    {
      user.LastName = lastName;
    }

    // Email
    var emailResult = Email.Create(email);
    if (emailResult.IsInvalid())
    {
      validationErrors.AddRange(emailResult.ValidationErrors);
    }
    else
    {
      user.Email = emailResult.Value;
    }

    // Password
    var passwordErrors = PasswordPolicy.Validate(password);
    if (passwordErrors.Any())
    {
      var passwordValidationErrors = passwordErrors
          .Select(error => new ValidationError
          {
            Identifier = nameof(password),
            ErrorMessage = error
          })
          .ToList();
      validationErrors.AddRange(passwordValidationErrors);
    }
    else
    {
      user.PasswordHash = hasher.Hash(password);
    }

    if (validationErrors.Any())
    {
      return Result.Invalid(validationErrors);
    }

    return Result.Success(user);
  }

  // -----------------------------
  // Update methods
  // -----------------------------

  public Result SetEmail(string newEmail)
  {
    var emailResult = Email.Create(newEmail);
    if (emailResult.IsInvalid())
    {
      return Result.Invalid(emailResult.ValidationErrors);
    }

    Email = emailResult.Value;
    return Result.Success();
  }

  public Result SetPassword(string newPassword, IPasswordHasher hasher)
  {
    var errors = PasswordPolicy.Validate(newPassword);
    if (errors.Any())
    {
      var validationErrors = errors
          .Select(error => new ValidationError
          {
            Identifier = nameof(newPassword),
            ErrorMessage = error
          })
          .ToList();
      return Result.Invalid(validationErrors);
    }

    PasswordHash = hasher.Hash(newPassword);
    return Result.Success();
  }

  public Result UpdateFirstName(string firstName)
  {
    if (string.IsNullOrWhiteSpace(firstName))
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(firstName),
        ErrorMessage = "First name cannot be empty"
      });
    }

    FirstName = firstName;
    return Result.Success();
  }
  public Result UpdateLastName(string lastName)
  {
    if (string.IsNullOrWhiteSpace(lastName))
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(lastName),
        ErrorMessage = "Last name cannot be empty"
      });
    }

    LastName = lastName;
    return Result.Success();
  }
  public Result UpdateProfilePhoto(string? path)
  {
    if (string.IsNullOrWhiteSpace(path))
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(path),
        ErrorMessage = "Profile photo path cannot be empty"
      });
    }

    ProfilePhotoPath = path;
    return Result.Success();
  }
}
