using Ardalis.SharedKernel;
using tutoriaBE.Core.CourseAggregate;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.UserAggregate.Entities;
using tutoriaBE.Core.UserAggregate.Specifications;

namespace tutoriaBE.UseCases.Tutors.TeachCourse;

public class TeachCourseTutorHandler : ICommandHandler<TeachCourseTutorCommand, Result<TutorCourseDTO>>
{
  private readonly IRepository<Course> _courseRepository;
  private readonly IRepository<User> _userRepository;

  public TeachCourseTutorHandler(IRepository<Course> courseRepository, IRepository<User> userRepository)
  {
    _courseRepository = courseRepository;
    _userRepository = userRepository;
  }

  public async Task<Result<TutorCourseDTO>> Handle(TeachCourseTutorCommand request,
    CancellationToken cancellationToken)
  {
    var course = await _courseRepository.GetByIdAsync(request.courseId, cancellationToken);
    if (course == null)
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(request.courseId),
        ErrorMessage = "Course not found."
      });

    var spec = new UserByIdSpec(request.tutorId);
    var user = await _userRepository.FirstOrDefaultAsync(spec, cancellationToken);

    if (user == null || user.TutorProfile == null)
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(request.tutorId),
        ErrorMessage = "User is not a tutor or does not exist."
      });

    var existingRelation = await _userRepository.AnyAsync(
        new TutorCourseExistsSpec(request.tutorId, request.courseId),
        cancellationToken);

    if (existingRelation)
    {
      return Result.Invalid(new ValidationError
      {
        Identifier = nameof(request.courseId),
        ErrorMessage = "Tutor already teaches this course."
      });
    }

    var tutor = user.TutorProfile;
    var result = tutor.TeachCourse(request.courseId, request.hourlyPrice);
    if (!result.IsSuccess)
      return Result.Invalid(result.ValidationErrors);

    await _userRepository.UpdateAsync(user, cancellationToken);

    var tutorCourse = result.Value;

    var dto = new TutorCourseDTO(
        user.FirstName,
        course.Title,
        tutorCourse.HourlyPrice
    );

    return Result.Success(dto);
  }
}
