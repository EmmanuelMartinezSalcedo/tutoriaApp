using tutoriaBE.Core.CourseAggregate;

namespace tutoriaBE.UseCases.Courses.Create;
public class CreateCourseHandler : ICommandHandler<CreateCourseCommand, Result<int>>
{
  private readonly IRepository<Course> _repository;

  public CreateCourseHandler(IRepository<Course> repository)
  {
    _repository = repository;
  }

  public async Task<Result<int>> Handle(CreateCourseCommand request,
    CancellationToken cancellationToken)
  {
    if (!string.IsNullOrEmpty(request.title))
    {
      var existingCourse = await _repository.FirstOrDefaultAsync(
          new CourseByTitleSpec(request.title), cancellationToken);

      if (existingCourse != null)
      {
        return Result.Invalid(new ValidationError
        {
          Identifier = nameof(request.title),
          ErrorMessage = "This course already exists."
        });
      }
    }

    var courseResult = Course.Create(request.title, request.description);
    if (courseResult.IsInvalid())
    {
      return Result.Invalid(courseResult.ValidationErrors.ToArray());
    }

    var createdItem = await _repository.AddAsync(courseResult.Value, cancellationToken);
    return Result.Success(createdItem.Id);
  }
}
