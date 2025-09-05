using FluentValidation;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.UserAggregate.Policies;

namespace tutoriaBE.Web.Sessions;

public class BookSessionValidator : Validator<BookSessionRequest>
{
  public BookSessionValidator()
  {
    RuleFor(x => x.TutorId)
        .Custom((tutorId, context) =>
        {
          var errors = IdPolicy.Validate(tutorId);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });

    RuleFor(x => x.StudentId)
        .Custom((studentId, context) =>
        {
          var errors = IdPolicy.Validate(studentId);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });

    RuleFor(x => x.CourseId)
        .Custom((courseId, context) =>
        {
          var errors = IdPolicy.Validate(courseId);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });

    RuleFor(x => x.Day)
        .Custom((dayString, context) =>
        {
          ScheduleSlotDayOfWeek? dayEnum = null;

          try
          {
            dayEnum = ScheduleSlotDayOfWeek.FromName(dayString, ignoreCase: true);
          }
          catch
          {
            context.AddFailure($"Invalid day value: {dayString}");
            return;
          }

          var errors = DayOfWeekPolicy.Validate(dayEnum!);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });

    RuleFor(x => x.ScheduleSlotIds)
        .Custom((slotIds, context) =>
        {
          if (slotIds != null)
          {
            foreach (var slotId in slotIds)
            {
              var errors = IdPolicy.Validate(slotId);
              foreach (var error in errors)
              {
                context.AddFailure(error);
              }
            }
          }
        });
  }

}
