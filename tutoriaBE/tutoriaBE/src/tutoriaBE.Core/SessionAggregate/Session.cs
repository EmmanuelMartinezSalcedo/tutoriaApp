using tutoriaBE.Core.CourseAggregate;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.UserAggregate.Policies;

namespace tutoriaBE.Core.SessionAggregate;

public class Session : EntityBase, IAggregateRoot
{
  public int TutorId { get; private set; }
  public int StudentId { get; private set; }
  public int CourseId { get; private set; }
  public SessionStatus Status { get; private set; } = SessionStatus.Pending;
  public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

  // -----------------------------
  // Navigation properties
  // -----------------------------

  public virtual List<SessionSlot>? SessionSlots { get; private set; }
  public virtual Review? Review { get; private set; }
  public virtual Chat? Chat { get; private set; }

  // -----------------------------
  // Update methods
  // -----------------------------

  protected Session() { } // EF Core

  public Session(int tutorId, int studentId, int courseId)
  {
    TutorId = Guard.Against.NegativeOrZero(tutorId, nameof(tutorId));
    StudentId = Guard.Against.NegativeOrZero(studentId, nameof(studentId));
    CourseId = Guard.Against.NegativeOrZero(courseId, nameof(courseId));
    CreatedAt = DateTime.UtcNow;
  }

  // -----------------------------
  // Update methods
  // ----------------------------

  public static Result<Session> BookSession(int tutorId, int studentId, int courseId, ScheduleSlotDayOfWeek day, List<int> scheduleSlotIds)
  {
    var validationErrors = new List<ValidationError>();

    // TutorId
    var tutorIdErrors = IdPolicy.Validate(tutorId);
    if (tutorIdErrors.Any())
      validationErrors.AddRange(tutorIdErrors.Select(e => new ValidationError { Identifier = nameof(tutorId), ErrorMessage = e }));

    // StudentId
    var studentIdErrors = IdPolicy.Validate(studentId);
    if (studentIdErrors.Any())
      validationErrors.AddRange(studentIdErrors.Select(e => new ValidationError { Identifier = nameof(studentId), ErrorMessage = e }));

    // CourseId
    var courseIdErrors = IdPolicy.Validate(courseId);
    if (courseIdErrors.Any())
      validationErrors.AddRange(courseIdErrors.Select(e => new ValidationError { Identifier = nameof(courseId), ErrorMessage = e }));

    // Day
    var dayErrors = DayOfWeekPolicy.Validate(day);
    if (dayErrors.Any())
      validationErrors.AddRange(dayErrors.Select(e => new ValidationError { Identifier = nameof(day), ErrorMessage = e }));

    // ScheduleSlotIds
    if (scheduleSlotIds != null)
    {
      foreach (var slotId in scheduleSlotIds)
      {
        var slotIdErrors = IdPolicy.Validate(slotId);
        if (slotIdErrors.Any())
          validationErrors.AddRange(slotIdErrors.Select(e => new ValidationError { Identifier = nameof(scheduleSlotIds), ErrorMessage = e }));
      }
    }

    if (validationErrors.Any())
      return Result<Session>.Invalid(validationErrors);

    // Crear la sesión
    var session = new Session(tutorId, studentId, courseId);

    // Agregar SessionSlots
    if (scheduleSlotIds != null && scheduleSlotIds.Any())
    {
      foreach (var slotId in scheduleSlotIds)
      {
        session.SessionSlots!.Add(new SessionSlot(session.Id, slotId));
      }
    }

    return Result.Success(session);
  }

}
