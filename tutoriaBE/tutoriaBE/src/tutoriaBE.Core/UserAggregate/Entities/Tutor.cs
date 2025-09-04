using tutoriaBE.Core.CourseAggregate;
using tutoriaBE.Core.SessionAggregate;

namespace tutoriaBE.Core.UserAggregate.Entities;

public class Tutor : EntityBase
{
  public string? Bio { get; private set; }

  // -----------------------------
  // Navigation properties
  // -----------------------------
  public List<TutorCourse>? TutorCourses { get; private set; }
  public List<ScheduleSlot>? ScheduleSlots { get; private set; }

  // -----------------------------
  // Constructors
  // -----------------------------
  private Tutor() { } // EF Core

  public Tutor(int userId, string? bio = null)
  {
    Id = Guard.Against.NegativeOrZero(userId, nameof(userId));
    UpdateBio(bio);
  }

  // -----------------------------
  // Update methods
  // -----------------------------
  public Result UpdateBio(string? bio)
  {
    if (!string.IsNullOrWhiteSpace(bio) && bio.Length > 1000)
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(bio),
        ErrorMessage = "Bio cannot exceed 1000 characters"
      });
    }

    Bio = string.IsNullOrWhiteSpace(bio) ? null : bio.Trim();
    return Result.Success();
  }
}
