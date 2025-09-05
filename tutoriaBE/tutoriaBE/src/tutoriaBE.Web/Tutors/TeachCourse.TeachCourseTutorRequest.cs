using System.ComponentModel.DataAnnotations;

namespace tutoriaBE.Web.Tutors;

public class TeachCourseTutorRequest
{
  public const string Route = "/Tutors/Teach";

  [Required]
  public int? TutorId { get; set; }

  [Required]
  public int? CourseId { get; set; }

  [Required]
  public decimal? HourlyPrice { get; set; }
}
