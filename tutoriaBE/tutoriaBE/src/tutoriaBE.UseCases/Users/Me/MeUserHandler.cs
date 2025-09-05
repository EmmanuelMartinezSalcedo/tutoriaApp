using tutoriaBE.Core.Interfaces;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.UseCases.Users.Me;

namespace tutoriaBE.UseCases.Users.Me;
public class MeUserHandler : IQueryHandler<MeUserQuery, Result<UserDTO>>
{
  private readonly IRepository<User> _repository;

  public MeUserHandler(IRepository<User> repository)
  {
    _repository = repository;
  }

  public async Task<Result<UserDTO>> Handle(MeUserQuery request, CancellationToken cancellationToken)
  {
    var user = await _repository.GetByIdAsync(request.id, cancellationToken);

    if (user == null)
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(request.id),
        ErrorMessage = "User must be logged in"
      });
    }

    var userDto = new UserDTO(
        user.Id,
        user.FirstName,
        user.LastName
    );

    return Result.Success(userDto);
  }
}
