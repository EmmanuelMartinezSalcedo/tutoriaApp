using FluentValidation;
using tutoriaBE.Core.CourseAggregate.Policies;

namespace tutoriaBE.Web.Courses;

public class CreateCourseValidator : Validator<CreateCourseRequest>
{
  public CreateCourseValidator()
  {
    RuleFor(x => x.Title)
        .Custom((title, context) =>
        {
          var errors = TitlePolicy.Validate(title!);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });

    RuleFor(x => x.Description)
        .Custom((description, context) =>
        {
          var errors = DescriptionPolicy.Validate(description!);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });
  }
}
