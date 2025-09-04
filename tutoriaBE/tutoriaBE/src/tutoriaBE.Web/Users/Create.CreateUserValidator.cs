using Ardalis.SharedKernel;
using FluentValidation;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.UserAggregate.Policies;
using tutoriaBE.Infrastructure.Data.Config;

namespace tutoriaBE.Web.Users;

public class CreateUserValidator : Validator<CreateUserRequest>
{
  private readonly IRepository<User> _repository;

  public CreateUserValidator(IRepository<User> repository)
  {
    _repository = repository;

    RuleFor(x => x.FirstName)
        .Custom((firstName, context) =>
        {
          if (firstName is null)
            return;
          var errors = FirstNamePolicy.Validate(firstName);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });

    RuleFor(x => x.LastName)
        .Custom((lastName, context) =>
        {
          if (lastName is null)
            return;
          var errors = LastNamePolicy.Validate(lastName);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });

    RuleFor(x => x.Email)
        .CustomAsync(async (email, context, cancellation) =>
        {
          if (email is null) return;

          foreach (var error in EmailPolicy.Validate(email))
            context.AddFailure(error);

          if (EmailPolicy.IsValidFormat(email))
          {
            var existingUser = await _repository.FirstOrDefaultAsync(
                new UserByEmailSpec(email), cancellation);
            if (existingUser != null)
              context.AddFailure("This email is already registered.");
          }
        });


    RuleFor(x => x.Password)
        .Custom((password, context) =>
        {
          if (password is null)
            return;
          var errors = PasswordPolicy.Validate(password);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });
  }
}
