namespace tutoriaBE.Web.Courses;

public class CreateCourseResponse(string title, string description)
{
  public string Title { get; set; } = title;
  public string Description { get; set; } = description;
}
