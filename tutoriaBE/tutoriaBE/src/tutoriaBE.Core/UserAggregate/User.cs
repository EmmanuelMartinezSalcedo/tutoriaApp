using tutoriaBE.Core.UserAggregate.ValueObjects;
using tutoriaBE.Core.UserAggregate.Entities;

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

  // -----------------------------
  // Constructors
  // -----------------------------

  private User() { } // EF Core

  public User(string firstName, string lastName, string email, string password, IPasswordHasher hasher, string? profilePhotoPath = null)
  {
    UpdateFirstName(firstName);
    UpdateLastName(lastName);

    var emailResult = Email.Create(email);
    if (emailResult.IsInvalid())
      throw new ArgumentException(emailResult.Errors.First(), nameof(email));

    Email = emailResult.Value;

    PasswordHash = hasher.Hash(password);
    UpdateProfilePhoto(profilePhotoPath);
    CreatedAt = DateTime.UtcNow;
  }

  // -----------------------------
  // Update methods
  // -----------------------------

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

  public Result UpdatePassword(string currentPassword, string newPassword, IPasswordHasher hasher)
  {
    Guard.Against.NullOrEmpty(currentPassword, nameof(currentPassword));
    Guard.Against.NullOrEmpty(newPassword, nameof(newPassword));
    Guard.Against.Null(hasher, nameof(hasher));

    if (!hasher.Verify(currentPassword, PasswordHash))
    {
      return Result.Unauthorized("Current password is incorrect");
    }

    if (newPassword.Length < 8)
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(newPassword),
        ErrorMessage = "New password must be at least 8 characters"
      });
    }

    if (!newPassword.Any(char.IsUpper) ||
        !newPassword.Any(char.IsLower) ||
        !newPassword.Any(char.IsDigit))
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(newPassword),
        ErrorMessage = "New password must contain uppercase, lowercase and numbers"
      });
    }

    if (hasher.Verify(newPassword, PasswordHash))
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(newPassword),
        ErrorMessage = "New password must be different than current"
      });
    }

    PasswordHash = hasher.Hash(newPassword);

    return Result.Success();
  }
}
