using tutoriaBE.UseCases.Tutors.TeachCourse;

namespace tutoriaBE.Web.Tutors;

public class TeachCourse(IMediator _mediator) : Endpoint<TeachCourseTutorRequest, TeachCourseTutorResponse>
{
  public override void Configure()
  {
    Post(TeachCourseTutorRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new TeachCourseTutorRequest
      {
        TutorId = 1,
        CourseId = 2,
        HourlyPrice = 50.0m,
      };
    });
  }

  public override async Task HandleAsync(
    TeachCourseTutorRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new TeachCourseTutorCommand((request.TutorId!.Value), (request.CourseId!.Value), (request.HourlyPrice!.Value)), cancellationToken);

    if (result.IsSuccess)
    {
      var dto = result.Value;
      Response = new TeachCourseTutorResponse
      {
        FirstName = dto.FirstName,
        Title = dto.Title,
        HourlyPrice = dto.HourlyPrice
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
