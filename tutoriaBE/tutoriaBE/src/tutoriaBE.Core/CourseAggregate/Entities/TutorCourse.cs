using tutoriaBE.Core.UserAggregate;

namespace tutoriaBE.Core.CourseAggregate;

public class TutorCourse : EntityBase
{
  public int CourseId { get; private set; }
  public int TutorId { get; private set; }
  public decimal HourlyPrice { get; private set; } = default!;

  // -----------------------------
  // Navigation properties
  // -----------------------------

  // -----------------------------
  // Constructors
  // -----------------------------
  private TutorCourse() { } // EF Core

  public TutorCourse(int courseId, int tutorId, decimal hourlyPrice)
  {
    CourseId = Guard.Against.NegativeOrZero(courseId, nameof(courseId));
    TutorId = Guard.Against.NegativeOrZero(tutorId, nameof(tutorId));
    UpdateHourlyPrice(hourlyPrice);
  }

  // -----------------------------
  // Update methods
  // -----------------------------
  public Result UpdateHourlyPrice(decimal hourlyPrice)
  {
    if (hourlyPrice <= 0)
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(hourlyPrice),
        ErrorMessage = "Hourly price must be greater than 0"
      });
    }

    if (hourlyPrice > 1000)
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(hourlyPrice),
        ErrorMessage = "Hourly price cannot exceed $1 000"
      });
    }

    HourlyPrice = hourlyPrice;
    return Result.Success();
  }
}
