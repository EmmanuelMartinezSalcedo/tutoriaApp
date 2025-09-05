using System.ComponentModel.DataAnnotations;

namespace tutoriaBE.Web.Users;

public class LoginUserRequest
{
  public const string Route = "/Users/login";

  [Required]
  [EmailAddress]
  public string? Email { get; set; }

  [Required]
  public string? Password { get; set; }
}
