using FluentValidation;
using tutoriaBE.Core.UserAggregate.Policies;

namespace tutoriaBE.Web.Tutors;

public class TeachCourseTutorValidator : Validator<TeachCourseTutorRequest>
{
  public TeachCourseTutorValidator()
  {
    RuleFor(x => x.TutorId)
        .Custom((tutorId, context) =>
        {
          var errors = IdPolicy.Validate(tutorId!);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });

    RuleFor(x => x.CourseId)
        .Custom((courseId, context) =>
        {
          var errors = IdPolicy.Validate(courseId!);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });

    RuleFor(x => x.HourlyPrice)
        .Custom((hourlyPrice, context) =>
        {
          var errors = HourlyPricePolicy.Validate(hourlyPrice!);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });
  }
}
