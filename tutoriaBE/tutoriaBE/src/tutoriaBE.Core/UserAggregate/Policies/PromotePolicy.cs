namespace tutoriaBE.Core.UserAggregate.Policies;

public static class PromotePolicy
{
  public static bool CanPromoteToTutor(UserRole role)
  {
    return role == UserRole.Student;
  }

  public static List<string> Validate(UserRole role)
  {
    var errors = new List<string>();
    if (!CanPromoteToTutor(role))
      errors.Add("Only Students can be promoted to Tutor.");
    return errors;
  }
}
