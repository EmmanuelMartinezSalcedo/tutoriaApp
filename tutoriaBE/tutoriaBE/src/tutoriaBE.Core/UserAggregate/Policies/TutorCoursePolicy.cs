using tutoriaBE.Core.UserAggregate.Entities;

namespace tutoriaBE.Core.UserAggregate.Policies;

public static class TutorCoursePolicy
{
  public static bool TutorIsNotTeachingCourse(Tutor tutor, int courseId)
  {
    return tutor.TutorCourses == null || !tutor.TutorCourses.Any(tc => tc.CourseId == courseId);
  }


  public static List<string> Validate(Tutor tutor, int courseId)
  {
    var errors = new List<string>();

    if (!TutorIsNotTeachingCourse(tutor, courseId))
      errors.Add("Tutor is already teaching this course.");

    return errors;
  }
}
