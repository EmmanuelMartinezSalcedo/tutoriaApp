using System.ComponentModel.DataAnnotations;

namespace tutoriaBE.Web.Users;
public class CreateUserRequest
{
  public const string Route = "/Users";

  [Required]
  public string? FirstName { get; set; }

  [Required]
  public string? LastName { get; set; }

  [Required]
  [EmailAddress]
  public string? Email { get; set; }

  [Required]
  public string? Password { get; set; }
}
