using Microsoft.AspNetCore.Authorization;
using tutoriaBE.UseCases.Users.Promote;

namespace tutoriaBE.Web.Users;

[Authorize]
public class Promote(IMediator _mediator) : Endpoint<PromoteUserRequest, PromoteUserResponse>
{
  public override void Configure()
  {
    Post(PromoteUserRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new PromoteUserRequest
      {
        Id = 1
      };
    });
  }

  public override async Task HandleAsync(
  PromoteUserRequest request,
  CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new PromoteUserQuery(request.Id!.Value), cancellationToken);

    if (result.IsSuccess)
    {
      var dto = result.Value;
      Response = new PromoteUserResponse
      {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Role = dto.Role.ToString()
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
