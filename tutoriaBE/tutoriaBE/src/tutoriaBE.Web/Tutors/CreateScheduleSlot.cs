using tutoriaBE.Core.UserAggregate;
using tutoriaBE.UseCases.Tutors.CreateScheduleSlot;

namespace tutoriaBE.Web.Tutors;

public class CreateScheduleSlot(IMediator _mediator) : Endpoint<CreateScheduleSlotTutorRequest, CreateScheduleSlotTutorResponse>
{
  public override void Configure()
  {
    Post(CreateScheduleSlotTutorRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new CreateScheduleSlotTutorRequest
      {
        TutorId = 1,
        Day = "Monday",
        StartHour = 7,
      };
    });
  }

  public override async Task HandleAsync(
    CreateScheduleSlotTutorRequest request,
    CancellationToken cancellationToken)
  {
    ScheduleSlotDayOfWeek dayEnum;
    try
    {
      dayEnum = ScheduleSlotDayOfWeek.FromName(request.Day!, ignoreCase: true);
    }
    catch
    {
      throw new ArgumentException($"Invalid day value: {request.Day}");
    }

    var result = await _mediator.Send(
        new CreateScheduleSlotTutorCommand(
            request.TutorId!.Value,
            dayEnum,
            request.StartHour!.Value
        ),
        cancellationToken
    );

    if (result.IsSuccess)
    {
      var dto = result.Value;
      Response = new CreateScheduleSlotTutorResponse
      {
        Day = dto.DayOfWeek.Name,
        StartTime = dto.StartHour
      };

      return;
    }
    switch (result.Status)
    {
      case ResultStatus.Invalid:
        foreach (var error in result.ValidationErrors)
        {
          AddError(error.ErrorMessage, error.Identifier);
        }
        await SendErrorsAsync(400, cancellationToken);
        break;

      case ResultStatus.NotFound:
        await SendNotFoundAsync(cancellationToken);
        break;

      case ResultStatus.Unauthorized:
        await SendUnauthorizedAsync(cancellationToken);
        break;

      case ResultStatus.Forbidden:
        await SendForbiddenAsync(cancellationToken);
        break;

      default:
        AddError("An unexpected error occurred.");
        await SendErrorsAsync(500, cancellationToken);
        break;
    }
  }
}
