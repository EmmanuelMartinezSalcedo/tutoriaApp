using Ardalis.Result;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.UseCases.Sessions;
using tutoriaBE.UseCases.Sessions.Book;

namespace tutoriaBE.Web.Sessions;

public class Book(IMediator _mediator)
  : Endpoint<BookSessionRequest, BookSessionResponse>
{
  public override void Configure()
  {
    Post(BookSessionRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new BookSessionRequest
      {
        TutorId = 1,
        StudentId = 2,
        CourseId = 3,
        Day = "Monday",
        ScheduleSlotIds = new List<int> { 101, 102 }
      };
    });
  }

  public override async Task HandleAsync(
    BookSessionRequest request,
    CancellationToken cancellationToken)
  {
    // Convert day string to enum
    var dayEnum = ScheduleSlotDayOfWeek.FromName(request.Day!, ignoreCase: true);

    var command = new BookSessionCommand(
      request.TutorId!.Value,
      request.StudentId!.Value,
      request.CourseId!.Value,
      dayEnum,
      request.ScheduleSlotIds!
    );

    var result = await _mediator.Send(command, cancellationToken);

    if (result.IsSuccess)
    {
      var dto = result.Value;
      Response = new BookSessionResponse(dto.day, dto.startHour, dto.endHour);
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
