namespace tutoriaBE.Core.UserAggregate.Specifications;

public class UserByIdSpec : Specification<User>
{
  public UserByIdSpec(int userId)
  {
    Query.Where(u => u.Id == userId)
         .Include(u => u.TutorProfile!);
  }
}
