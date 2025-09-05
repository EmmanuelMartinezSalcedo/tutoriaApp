using System.ComponentModel.DataAnnotations;

namespace tutoriaBE.Web.Courses;

public class CreateCourseRequest
{
  public const string Route = "/Courses";

  [Required]
  public string? Title { get; set; }

  [Required]
  public string? Description { get; set; }
}

