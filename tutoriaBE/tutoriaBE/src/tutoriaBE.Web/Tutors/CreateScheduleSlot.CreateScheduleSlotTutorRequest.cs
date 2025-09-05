using System.ComponentModel.DataAnnotations;

namespace tutoriaBE.Web.Tutors;

public class CreateScheduleSlotTutorRequest
{
  public const string Route = "/Tutors/CreateScheduleSlot";

  [Required]
  public int? TutorId { get; set; }

  [Required]
  public string? Day { get; set; }

  [Required]
  public int? StartHour { get; set; }
}
