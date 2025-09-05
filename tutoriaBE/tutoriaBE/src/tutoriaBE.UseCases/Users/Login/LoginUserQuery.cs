
namespace tutoriaBE.UseCases.Users.Login;
public record LoginUserQuery(string email, string password) : IQuery<Result<LoginResultDTO>>;
