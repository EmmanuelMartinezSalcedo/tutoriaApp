using tutoriaBE.Core.UserAggregate;
using tutoriaBE.UseCases.Users.Create;

public class CreateUserHandler : ICommandHandler<CreateUserCommand, Result<int>>
{
  private readonly IRepository<User> _repository;
  private readonly IPasswordHasher _hasher;

  public CreateUserHandler(IRepository<User> repository, IPasswordHasher hasher)
  {
    _repository = repository;
    _hasher = hasher;
  }

  public async Task<Result<int>> Handle(CreateUserCommand request,
    CancellationToken cancellationToken)
  {
    if (!string.IsNullOrEmpty(request.email) && EmailPolicy.IsValidFormat(request.email))
    {
      var existingUser = await _repository.FirstOrDefaultAsync(
          new UserByEmailSpec(request.email), cancellationToken);

      if (existingUser != null)
      {
        return Result.Invalid(new ValidationError
        {
          Identifier = nameof(request.email),
          ErrorMessage = "This email is already registered."
        });
      }
    }

    var userResult = User.Create(request.firstName, request.lastName, request.email, request.password, _hasher);
    if (userResult.IsInvalid())
    {
      return Result.Invalid(userResult.ValidationErrors.ToArray());
    }

    var createdItem = await _repository.AddAsync(userResult.Value, cancellationToken);
    return Result.Success(createdItem.Id);
  }
}
