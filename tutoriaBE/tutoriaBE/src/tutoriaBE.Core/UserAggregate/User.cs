using System.Collections.Generic;
using Ardalis.Result;
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

  public virtual Tutor? TutorProfile { get; private set; }
  public virtual List<Message>? Messages { get; private set; }

  // -----------------------------
  // Constructors
  // -----------------------------

  protected User() { } // EF Core

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
  // Bussiness methods
  // -----------------------------
  public Result Promote()
  {
    var validationErrors = new List<ValidationError>();
    var promotionErrors = PromotePolicy.Validate(Role);
    if (promotionErrors.Any())
    {
      var promotionValidationErrors = promotionErrors
          .Select(error => new ValidationError
          {
            Identifier = nameof(Role),
            ErrorMessage = error
          })
          .ToList();
      validationErrors.AddRange(promotionValidationErrors);
    }
    else
    {
      var tutor = new Tutor(Id);
      TutorProfile = tutor;
      Role = UserRole.Tutor;
    }

    if (validationErrors.Any())
    {
      return Result.Invalid(validationErrors);
    }

    return Result.Success();
  }
}
