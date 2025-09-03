namespace tutoriaBE.Core.CourseAggregate;

public class Course : EntityBase, IAggregateRoot
{
  public string Title { get; private set; } = default!;
  public string Description { get; private set; } = default!;

  // -----------------------------
  // Navigation properties
  // -----------------------------

  public List<TutorCourse>? TutorCourses { get; private set; };

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

  public Result UpdateTitle(string title)
  {
    if (string.IsNullOrWhiteSpace(title))
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(title),
        ErrorMessage = "Title cannot be empty"
      });
    }

    Title = title;
    return Result.Success();
  }

  public Result UpdateDescription(string description)
  {
    if (string.IsNullOrWhiteSpace(description))
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(description),
        ErrorMessage = "Description cannot be empty"
      });
    }

    Description = description;
    return Result.Success();
  }
}
