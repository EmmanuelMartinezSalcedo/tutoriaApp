using tutoriaBE.Core.CourseAggregate;
using tutoriaBE.Core.SessionAggregate;
using tutoriaBE.Core.UserAggregate.Policies;

namespace tutoriaBE.Core.UserAggregate.Entities;

public class Tutor : EntityBase
{
  public string? Bio { get; private set; }

  // -----------------------------
  // Navigation properties
  // -----------------------------
  public virtual List<TutorCourse>? TutorCourses { get; private set; }
  public virtual List<ScheduleSlot>? ScheduleSlots { get; private set; }

  // -----------------------------
  // Constructors
  // -----------------------------
  protected Tutor() { } // EF Core

  public Tutor(int userId)
  {
    Id = Guard.Against.NegativeOrZero(userId, nameof(userId));
  }

  // -----------------------------
  // Bussiness methods
  // -----------------------------

  public Result<TutorCourse> TeachCourse(int courseId, decimal hourlyPrice)
  {
    var validationErrors = new List<ValidationError>();

    var courseErrors = TutorCoursePolicy.Validate(this, courseId);
    if (courseErrors.Any())
      validationErrors.AddRange(courseErrors.Select(e => new ValidationError
      {
        Identifier = nameof(courseId),
        ErrorMessage = e
      }));

    var priceErrors = HourlyPricePolicy.Validate(hourlyPrice);
    if (priceErrors.Any())
      validationErrors.AddRange(priceErrors.Select(e => new ValidationError
      {
        Identifier = nameof(hourlyPrice),
        ErrorMessage = e
      }));

    if (validationErrors.Any())
      return Result<TutorCourse>.Invalid(validationErrors);

    var existingCourse = TutorCourses?.FirstOrDefault(tc => tc.CourseId == courseId);
    if (existingCourse != null)
    {
      var priceResult = existingCourse.UpdateHourlyPrice(hourlyPrice);
      if (priceResult.IsInvalid())
        return Result<TutorCourse>.Invalid(priceResult.ValidationErrors);

      return Result<TutorCourse>.Success(existingCourse);
    }
    else
    {
      var newCourse = new TutorCourse(courseId, Id, hourlyPrice);
      TutorCourses ??= new List<TutorCourse>();
      TutorCourses.Add(newCourse);

      return Result<TutorCourse>.Success(newCourse);
    }
  }

  public Result<ScheduleSlot> CreateScheduleSlot(ScheduleSlotDayOfWeek day, int startHour)
  {
    var validationErrors = new List<ValidationError>();

    var startHourErrors = StartHourPolicy.Validate(startHour);
    if (startHourErrors.Any())
      validationErrors.AddRange(startHourErrors.Select(e => new ValidationError
      {
        Identifier = nameof(startHour),
        ErrorMessage = e
      }));

    if (validationErrors.Any())
      return Result<ScheduleSlot>.Invalid(validationErrors);

    var dayErrors = DayOfWeekPolicy.Validate(day);
    if (dayErrors.Any())
      validationErrors.AddRange(dayErrors.Select(e => new ValidationError
      {
        Identifier = nameof(day),
        ErrorMessage = e
      }));

    var existingSlot = ScheduleSlots?.FirstOrDefault(s => s.DayOfWeek == day && s.StartHour == startHour);
    if (existingSlot != null)
    {
      validationErrors.Add(new ValidationError
      {
        Identifier = nameof(startHour),
        ErrorMessage = "A schedule slot for this day and hour already exists."
      });
      return Result<ScheduleSlot>.Invalid(validationErrors);
    }



    var newSlot = new ScheduleSlot(Id, day, startHour);

    ScheduleSlots ??= new List<ScheduleSlot>();
    ScheduleSlots.Add(newSlot);

    return Result<ScheduleSlot>.Success(newSlot);
  }
}
