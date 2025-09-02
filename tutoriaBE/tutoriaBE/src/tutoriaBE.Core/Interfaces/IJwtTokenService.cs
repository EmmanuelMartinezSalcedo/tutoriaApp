using tutoriaBE.Core.UserAggregate;

namespace tutoriaBE.Core.Interfaces;

public interface IJwtTokenService
{
  JwtTokenResult GenerateToken(User user);
}

public record JwtTokenResult(string Token, DateTime Expiration);
