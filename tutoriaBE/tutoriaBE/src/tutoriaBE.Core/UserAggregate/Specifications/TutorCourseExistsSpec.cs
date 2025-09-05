using tutoriaBE.Core.UserAggregate;

public class TutorCourseExistsSpec : Specification<User>
{
  public TutorCourseExistsSpec(int tutorId, int courseId)
  {
    Query.Where(u => u.Id == tutorId &&
                u.TutorProfile!.TutorCourses!.Any(tc => tc.CourseId == courseId));
  }
}
