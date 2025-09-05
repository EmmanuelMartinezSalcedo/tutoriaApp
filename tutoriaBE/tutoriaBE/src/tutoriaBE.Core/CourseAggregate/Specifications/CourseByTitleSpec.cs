using tutoriaBE.Core.CourseAggregate;

public class CourseByTitleSpec : Specification<Course>
{
  public CourseByTitleSpec(string title)
  {
    Query.Where(c => c.Title == title);
  }
}
