using Microsoft.AspNetCore.Authorization;
using tutoriaBE.UseCases.Users.Me;

namespace tutoriaBE.Web.Users;

[Authorize]
public class Me(IMediator _mediator) : EndpointWithoutRequest<MeUserResponse>
{
  public override void Configure()
  {
    Get(MeUserRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.Response(
        StatusCodes.Status200OK,
        example: new MeUserResponse
        {
          Id = 1234,
          FirstName = "Jhon",
          LastName = "Doe"
        }
      );
    });
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    int.TryParse(userIdClaim, out var userId);

    var result = await _mediator.Send(new MeUserQuery(userId), ct);

    if (result.IsSuccess)
    {
      Response = new MeUserResponse
      {
        Id = result.Value.Id,
        FirstName = result.Value.FirstName,
        LastName = result.Value.LastName
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
        await SendErrorsAsync(400, ct);
        break;

      case ResultStatus.NotFound:
        await SendNotFoundAsync(ct);
        break;

      case ResultStatus.Unauthorized:
        await SendErrorsAsync(401, ct);
        break;

      case ResultStatus.Forbidden:
        await SendForbiddenAsync(ct);
        break;

      default:
        AddError("An unexpected error occurred.");
        await SendErrorsAsync(500, ct);
        break;
    }
  }
}
