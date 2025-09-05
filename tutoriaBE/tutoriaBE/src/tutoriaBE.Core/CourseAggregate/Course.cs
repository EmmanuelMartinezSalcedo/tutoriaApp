using tutoriaBE.Core.CourseAggregate.Policies;

namespace tutoriaBE.Core.CourseAggregate;

public class Course : EntityBase, IAggregateRoot
{
  public string Title { get; private set; } = default!;
  public string Description { get; private set; } = default!;

  // -----------------------------
  // Navigation properties
  // -----------------------------

  public virtual List<TutorCourse>? TutorCourses { get; private set; }

  // -----------------------------
  // Constructors
  // -----------------------------

  protected Course() { } // EF Core

  public static Result<Course> Create(string title, string description)
  {
    var validationErrors = new List<ValidationError>();
    var course = new Course();

    // Title
    var titleErrors = TitlePolicy.Validate(title);
    if (titleErrors.Any())
    {
      var titleValidationErrors = titleErrors
          .Select(error => new ValidationError
          {
            Identifier = nameof(title),
            ErrorMessage = error
          })
          .ToList();
      validationErrors.AddRange(titleValidationErrors);
    }
    else
    {
      course.Title = title;
    }

    // Description
    var descriptionErrors = DescriptionPolicy.Validate(description);
    if (descriptionErrors.Any())
    {
      var descriptionValidationErrors = descriptionErrors
          .Select(error => new ValidationError
          {
            Identifier = nameof(description),
            ErrorMessage = error
          })
          .ToList();
      validationErrors.AddRange(descriptionValidationErrors);
    }
    else
    {
      course.Description = description;
    }

    if (validationErrors.Any())
    {
      return Result.Invalid(validationErrors);
    }

    return Result.Success(course);
  }

  // -----------------------------
  // Update methods
  // -----------------------------

  
}
