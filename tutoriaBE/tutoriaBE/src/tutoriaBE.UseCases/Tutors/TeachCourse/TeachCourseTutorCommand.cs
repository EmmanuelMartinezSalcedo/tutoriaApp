namespace tutoriaBE.UseCases.Tutors.TeachCourse;
public record TeachCourseTutorCommand(int tutorId, int courseId, decimal hourlyPrice) : Ardalis.SharedKernel.ICommand<Result<TutorCourseDTO>>;

