using Microsoft.AspNetCore.Authorization;
using tutoriaBE.UseCases.Users.Me;

namespace tutoriaBE.Web.Users;

[Authorize]
public class Logout() : EndpointWithoutRequest<LogoutUserResponse>
{
  public override void Configure()
  {
    Post(LogoutUserRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.Response(
        StatusCodes.Status200OK,
        "Logout successful",
        example: new LoginUserResponse { Message = "Logout successful" }
      );
    });
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var authCookie = HttpContext.Request.Cookies["AuthToken"];

    if (string.IsNullOrEmpty(authCookie))
    {
      AddError("User must be logged in");
      await SendErrorsAsync(401, ct);
      return;
    }

    HttpContext.Response.Cookies.Delete("AuthToken");

    Response = new LogoutUserResponse
    {
      Message = "Logout successful"
    };
  }
}
