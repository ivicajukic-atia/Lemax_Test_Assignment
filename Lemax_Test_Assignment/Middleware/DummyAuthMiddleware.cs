using System.Security.Claims;

public class DummyAuthMiddleware
{
  private readonly RequestDelegate _next;

  public DummyAuthMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    // Simulate authentication
    context.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
    {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.Role, "Admin") // Simulate user roles if needed
        }, "DummyAuth"));

    await _next(context);
  }
}
