using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

public class DummyAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
  public DummyAuthHandler(
      IOptionsMonitor<AuthenticationSchemeOptions> options,
      ILoggerFactory logger,
      UrlEncoder encoder)
      : base(options, logger, encoder)
  {
  }

  protected override Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    // Authentication logic
    var claims = new[] { new Claim(ClaimTypes.Name, "TestUser") };
    var identity = new ClaimsIdentity(claims, "DummyAuth");
    var principal = new ClaimsPrincipal(identity);
    var ticket = new AuthenticationTicket(principal, "DummyAuth");

    return Task.FromResult(AuthenticateResult.Success(ticket));
  }
}
