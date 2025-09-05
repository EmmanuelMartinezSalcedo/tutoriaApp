namespace tutoriaBE.UseCases.Users.Create;
public record CreateUserCommand(string firstName, string lastName, string email, string password) : Ardalis.SharedKernel.ICommand<Result<int>>;
