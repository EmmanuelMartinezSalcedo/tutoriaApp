using tutoriaBE.Core.UserAggregate;

namespace tutoriaBE.UseCases.Users;
public record PromotedUserDTO(string FirstName, string LastName, UserRole Role);
