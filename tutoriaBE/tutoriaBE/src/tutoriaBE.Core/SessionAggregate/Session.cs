namespace tutoriaBE.Core.SessionAggregate;

public enum SessionStatus
{
  Pending,
  Completed,
  Canceled
}

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


  // -----------------------------
  // Update methods
  // -----------------------------

  private Session() { } // EF Core

  public Session(int tutorId, int studentId, int courseId)
  {
    UpdateTutorId(tutorId);
    UpdateStudentId(studentId);
    UpdateCourseId(courseId);
    UpdateStatus(SessionStatus.Pending);
    CreatedAt = DateTime.UtcNow;
  }
  
  // -----------------------------
  // Update methods
  // ----------------------------

  public Session UpdateTutorId(int tutorId)
  {
    TutorId = Guard.Against.NegativeOrZero(tutorId, nameof(tutorId));
    return this;
  }

  public Session UpdateStudentId(int studentId)
  {
    StudentId = Guard.Against.NegativeOrZero(studentId, nameof(studentId));
    return this;
  }

  public Session UpdateCourseId(int courseId)
  {
    CourseId = Guard.Against.NegativeOrZero(courseId, nameof(courseId));
    return this;
  }

  public Session UpdateStatus(SessionStatus status)
  {
    Status = Guard.Against.EnumOutOfRange(status, nameof(status));
    return this;
  }
}
