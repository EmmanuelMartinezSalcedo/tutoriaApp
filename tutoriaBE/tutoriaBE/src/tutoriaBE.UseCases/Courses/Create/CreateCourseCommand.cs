namespace tutoriaBE.UseCases.Courses.Create;
public record CreateCourseCommand(string title, string description) : Ardalis.SharedKernel.ICommand<Result<int>>;

