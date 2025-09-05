using tutoriaBE.UseCases.Courses.Create;

namespace tutoriaBE.Web.Courses;

public class Create(IMediator _mediator)
  : Endpoint<CreateCourseRequest, CreateCourseResponse>
{
  public override void Configure()
  {
    Post(CreateCourseRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new CreateCourseRequest
      {
        Title = "Mathematics",
        Description = "The formal science of numbers, quantity, space, and structure, using abstract objects and logical reasoning to discover patterns and relationships",
      };
    });
  }

  public override async Task HandleAsync(
    CreateCourseRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new CreateCourseCommand(request.Title!, request.Description!), cancellationToken);

    if (result.IsSuccess)
    {
      Response = new CreateCourseResponse(request.Title!, request.Description!);
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
