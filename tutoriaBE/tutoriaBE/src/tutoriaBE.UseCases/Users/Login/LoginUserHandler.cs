using tutoriaBE.Core.Interfaces;
using tutoriaBE.Core.UserAggregate;

namespace tutoriaBE.UseCases.Users.Login;

public class LoginUserHandler : IQueryHandler<LoginUserQuery, Result<LoginResultDTO>>
{
  private readonly IRepository<User> _repository;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IJwtTokenService _jwtTokenService;

  public LoginUserHandler(
      IRepository<User> repository,
      IPasswordHasher passwordHasher,
      IJwtTokenService jwtTokenService)
  {
    _repository = repository;
    _passwordHasher = passwordHasher;
    _jwtTokenService = jwtTokenService;
  }

  public async Task<Result<LoginResultDTO>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
  {
    var user = await _repository.FirstOrDefaultAsync(
        new UserByEmailSpec(request.email),
        cancellationToken);

    if (user == null)
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(request.email),
        ErrorMessage = "User not found"
      });
    }

    if (!_passwordHasher.Verify(request.password, user.PasswordHash))
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(request.password),
        ErrorMessage = "Incorrect password"
      });
    }

    var token = _jwtTokenService.GenerateToken(user);
    var resultDto = new LoginResultDTO(token.Token, token.Expiration);

    return Result.Success(resultDto);
  }
}
