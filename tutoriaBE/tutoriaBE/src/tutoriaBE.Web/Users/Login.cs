using tutoriaBE.UseCases.Users.Login;

namespace tutoriaBE.Web.Users;

public class Login(IMediator _mediator) : Endpoint<LoginUserRequest, LoginUserResponse>
{
  public override void Configure()
  {
    Post(LoginUserRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new LoginUserRequest
      {
        Email = "john.doe@example.com",
        Password = "StrongPassword123!"
      };

      s.Response(
        StatusCodes.Status200OK,
        "Login successful",
        example: new LoginUserResponse { Message = "Login successful" }
      );
    });
  }

  public override async Task HandleAsync(LoginUserRequest request, CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new LoginUserQuery(request.Email!, request.Password!), cancellationToken);

    if (result.IsSuccess)
    {
      var token = result.Value.Token;

      HttpContext.Response.Cookies.Append("AuthToken", token, new CookieOptions
      {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.Strict, // CSRF
        Expires = DateTime.UtcNow.AddHours(1)
      });

      Response = new LoginUserResponse
      {
        Message = "Login successful",
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
