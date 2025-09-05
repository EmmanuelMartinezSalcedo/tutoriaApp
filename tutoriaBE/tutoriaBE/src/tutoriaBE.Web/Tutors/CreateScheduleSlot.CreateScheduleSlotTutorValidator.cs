using FluentValidation;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.UserAggregate.Policies;

namespace tutoriaBE.Web.Tutors;

public class CreateScheduleSlotTutorValidator : Validator<CreateScheduleSlotTutorRequest>
{
  public CreateScheduleSlotTutorValidator()
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

    RuleFor(x => x.StartHour)
        .Custom((hour, context) =>
        {
          var errors = StartHourPolicy.Validate(hour);
          foreach (var error in errors)
          {
            context.AddFailure(error);
          }
        });
  }
}
