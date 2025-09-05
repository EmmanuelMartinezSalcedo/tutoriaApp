using FluentValidation;
using tutoriaBE.Core.UserAggregate.Policies;

namespace tutoriaBE.Web.Users;

public class LoginUserValidator : Validator<LoginUserRequest>
{
  public LoginUserValidator()
  {
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
          var errors = LoginPasswordPolicy.Validate(password!);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });
  }
}
