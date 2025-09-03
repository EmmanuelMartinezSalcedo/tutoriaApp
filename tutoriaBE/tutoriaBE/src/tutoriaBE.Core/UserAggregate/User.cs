using System.Collections.Generic;
using tutoriaBE.Core.Exceptions;

namespace tutoriaBE.Core.UserAggregate;

public enum UserRole
{
  Student,
  Tutor
}

public class User : EntityBase, IAggregateRoot
{
  public string FirstName { get; private set; } = default!;
  public string LastName { get; private set; } = default!;
  public string Email { get; private set; } = default!;
  public string PasswordHash { get; private set; } = default!;
  public string? ProfilePhotoPath { get; private set; }
  public UserRole Role { get; private set; }

  public DateTime CreatedAt { get; private set; }

  // -----------------------------
  // Navigation properties
  // -----------------------------



  // -----------------------------
  // Constructors
  // -----------------------------

  private User() { } // EF Core

  public User(string firstName, string lastName, string email, string password, UserRole role, IPasswordHasher hasher, string? profilePhotoPath = null)
  {
    UpdateFirstName(firstName);
    UpdateLastName(lastName);
    Email = Guard.Against.NullOrEmpty(email, nameof(email));
    PasswordHash = hasher.Hash(password);
    UpdateRole(role);
    UpdateProfilePhoto(profilePhotoPath);
    CreatedAt = DateTime.UtcNow;
  }

  // -----------------------------
  // Update methods
  // -----------------------------

  public User UpdateFirstName(string firstName)
  {
    FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
    return this;
  }
  public User UpdateLastName(string lastName)
  {
    LastName = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
    return this;
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

  public User UpdateProfilePhoto(string? path)
  {
    ProfilePhotoPath = path;
    return this;
  }
  public User UpdateRole(UserRole role)
  {
    Role = role;
    return this;
  }
}
