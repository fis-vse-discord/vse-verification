using System.Security;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace VseVerification.Security;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
    private readonly string _token;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<ApiKeyAuthenticationOptions> options,
        ILoggerFactory logger,
        ISystemClock clock,
        UrlEncoder encoder,
        IConfiguration configuration
    ) : base(options, logger, encoder, clock)
    {
        _token = configuration.GetValue<string>("ApiKey");

        if (string.IsNullOrEmpty(_token))
        {
            throw new SecurityException("ApiKey is not set in the application configuration!");
        }
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var header = Request.Headers["Authorization"].FirstOrDefault();
        var token = header?.Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (token != _token)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API Key supplied."));
        }

        var identity = new ClaimsIdentity(ApiKeyAuthenticationOptions.DefaultScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Options.Scheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}