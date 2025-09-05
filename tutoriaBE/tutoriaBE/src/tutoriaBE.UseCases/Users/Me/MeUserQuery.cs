namespace tutoriaBE.UseCases.Users.Me;
public record MeUserQuery(int id) : IQuery<Result<UserDTO>>;

