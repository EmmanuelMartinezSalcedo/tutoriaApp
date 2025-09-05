using Ardalis.SharedKernel;
using FluentValidation;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.UserAggregate.Policies;

namespace tutoriaBE.Web.Users;

public class CreateUserValidator : Validator<CreateUserRequest>
{
  public CreateUserValidator()
  {

    RuleFor(x => x.FirstName)
        .Custom((firstName, context) =>
        {
          var errors = FirstNamePolicy.Validate(firstName!);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });

    RuleFor(x => x.LastName)
        .Custom((lastName, context) =>
        {
          var errors = LastNamePolicy.Validate(lastName!);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });

    RuleFor(x => x.Email)
        .Custom((email, context) =>
        {
          var errors = EmailPolicy.Validate(email!);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });


    RuleFor(x => x.Password)
        .Custom((password, context) =>
        {
          var errors = PasswordPolicy.Validate(password!);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });
  }
}
