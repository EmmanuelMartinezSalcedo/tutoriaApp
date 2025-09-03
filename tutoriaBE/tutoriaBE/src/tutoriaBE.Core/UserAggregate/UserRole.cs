namespace tutoriaBE.Core.UserAggregate;

public class UserRole : SmartEnum<UserRole>
{
  public static readonly UserRole Student = new(nameof(Student), 1);
  public static readonly UserRole Tutor = new(nameof(Tutor), 2);

  protected UserRole(string name, int value) : base(name, value) { }
}

