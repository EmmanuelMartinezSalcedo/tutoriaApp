using tutoriaBE.UseCases.Users.Create;

namespace tutoriaBE.Web.Users;
public class Create(IMediator _mediator)
  : Endpoint<CreateUserRequest, CreateUserResponse>
{
  public override void Configure()
  {
    Post(CreateUserRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new CreateUserRequest
      {
        FirstName = "John",
        LastName = "Doe",
        Email = "john.doe@example.com",
        Password = "StrongPassword123!"
      };
    });
  }

  public override async Task HandleAsync(
    CreateUserRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new CreateUserCommand(request.FirstName!, request.LastName!, request.Email!, request.Password!), cancellationToken);

    if (result.IsSuccess)
    {
      Response = new CreateUserResponse(request.FirstName!);
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
