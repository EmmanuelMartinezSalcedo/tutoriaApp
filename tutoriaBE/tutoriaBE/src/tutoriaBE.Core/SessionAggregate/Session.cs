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


  // -----------------------------
  // Update methods
  // -----------------------------

  private Session() { } // EF Core

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

}
