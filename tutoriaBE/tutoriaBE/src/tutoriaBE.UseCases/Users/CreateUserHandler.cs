using FluentValidation;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.UseCases.Users.Create;

public class CreateUserHandler(IRepository<User> _repository, IPasswordHasher hasher, IValidator<CreateUserRequest> _validator)
  : ICommandHandler<CreateUserCommand, Result<int>>
{
  public async Task<Result<int>> Handle(CreateUserCommand request,
    CancellationToken cancellationToken)
  {
    var userResult = User.Create(request.firstName, request.lastName, request.email, request.password, hasher);

    if (userResult.IsInvalid())
    {
      return Result.Invalid(userResult.ValidationErrors.ToArray());
    }

    var createdItem = await _repository.AddAsync(userResult.Value, cancellationToken);
    return Result.Success(createdItem.Id);
  }
}
