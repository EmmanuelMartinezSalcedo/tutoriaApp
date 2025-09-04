using tutoriaBE.Core.UserAggregate;

public class UserByEmailSpec : Specification<User>
{
  public UserByEmailSpec(string email)
  {
    Query.Where(u => u.Email.Value == email);
  }
}
