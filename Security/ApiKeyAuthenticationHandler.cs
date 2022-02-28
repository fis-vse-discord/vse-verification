using System.Security;
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
        return Task.FromResult(AuthenticateResult.NoResult());
    }
}