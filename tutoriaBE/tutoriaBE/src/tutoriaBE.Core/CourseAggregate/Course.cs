namespace tutoriaBE.Core.CourseAggregate;

public class Course : EntityBase, IAggregateRoot
{
  public string Title { get; private set; } = default!;
  public string Description { get; private set; } = default!;

  // -----------------------------
  // Navigation properties
  // -----------------------------
  public ICollection<TutorCourse> TutorCourses { get; private set; } = new List<TutorCourse>();

  // -----------------------------
  // Constructors
  // -----------------------------

  private Course() { } // EF Core

  public Course(string title, string description)
  {
    UpdateTitle(title);
    UpdateDescription(description);
  }

  // -----------------------------
  // Update methods
  // -----------------------------

  public Course UpdateTitle(string title)
  {
    Title = Guard.Against.NullOrEmpty(title, nameof(title));
    return this;
  }

  public Course UpdateDescription(string description)
  {
    Description = Guard.Against.NullOrEmpty(description, nameof(description));
    return this;
  }
}
