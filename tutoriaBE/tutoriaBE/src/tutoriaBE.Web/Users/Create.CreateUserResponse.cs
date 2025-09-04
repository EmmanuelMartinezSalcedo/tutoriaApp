namespace tutoriaBE.Web.Users;

public class CreateUserResponse(int id, string name)
{
  public int Id { get; set; } = id;
  public string Name { get; set; } = name;
}
