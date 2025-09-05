using System.ComponentModel.DataAnnotations;

namespace tutoriaBE.Web.Sessions;

public class BookSessionRequest
{
  public const string Route = "/Sessions/Book";

  [Required]
  public int? TutorId { get; set; }

  [Required]
  public int? StudentId { get; set; }

  [Required]
  public int? CourseId { get; set; }

  [Required]
  public string? Day { get; set; }

  [Required]
  public List<int>? ScheduleSlotIds { get; set; }

}
