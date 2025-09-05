namespace tutoriaBE.UseCases.Users.Promote;
public record PromoteUserQuery(int id) : IQuery<Result<PromotedUserDTO>>;
