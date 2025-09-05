using System.ComponentModel.DataAnnotations;

namespace tutoriaBE.Web.Users;

public class PromoteUserRequest
{
  public const string Route = "/Users/promote";

  [Required]
  public int? Id { get; set; }
}
