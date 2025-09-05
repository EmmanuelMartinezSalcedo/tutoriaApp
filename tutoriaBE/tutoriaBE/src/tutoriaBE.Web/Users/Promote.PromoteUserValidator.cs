using Ardalis.SharedKernel;
using FluentValidation;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.UserAggregate.Policies;

namespace tutoriaBE.Web.Users;

public class PromoteUserValidator : Validator<PromoteUserRequest>
{
  private readonly IRepository<User> _repository;
  public PromoteUserValidator(IRepository<User> repository)
  {
    _repository = repository;

    RuleFor(x => x.Id)
        .Custom((id, context) =>
        {
          var errors = IdPolicy.Validate(id!);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });
  }
}
