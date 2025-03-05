using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace DartPointTracker.Api
{
    public class BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        private const string USERNAME = "username";
        private const string PASSWORD = "password";

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Check if Authorization header exists
            if (!Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues value))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
            }

            try
            {
                var authHeader = value.ToString();
                if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                {
                    return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
                }

                var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));

                var credentials = decodedCredentials.Split(':', 2);
                if (credentials.Length != 2)
                {
                    return Task.FromResult(AuthenticateResult.Fail("Invalid credentials format"));
                }

                var username = credentials[0];
                var password = credentials[1];

                if (username != USERNAME || password != PASSWORD)
                {
                    return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
                }

                // Create authenticated user
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, username),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "User")
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            catch (Exception ex)
            {
                return Task.FromResult(AuthenticateResult.Fail($"Authentication failed: {ex.Message}"));
            }
        }
    }
}