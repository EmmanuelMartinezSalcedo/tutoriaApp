using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.Interfaces;

namespace tutoriaBE.Infrastructure.Security;

public class JwtTokenService : IJwtTokenService
{
  private readonly IConfiguration _config;

  public JwtTokenService(IConfiguration config)
  {
    _config = config;
  }

  public JwtTokenResult GenerateToken(User user)
  {
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new Claim(JwtRegisteredClaimNames.Email, user.Email),
      new Claim("name", user.Name)
    };

    var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"]));

    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: expires,
        signingCredentials: creds
    );

    return new JwtTokenResult(new JwtSecurityTokenHandler().WriteToken(token), expires);
  }
}
