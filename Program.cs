using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using VseVerification.Configuration;
using VseVerification.Data;
using VseVerification.Security;
using VseVerification.Services;
using VseVerification.Services.Contract;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration(configuration => configuration.AddEnvironmentVariables());
builder.Host.ConfigureServices((host, services) =>
{
    services.Configure<VerificationConfiguration>(host.Configuration.GetRequiredSection("Verification"));
    
    services.AddDbContext<VseVerificationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("Default")!)
            .UseSnakeCaseNamingConvention()
    );

    services.AddTransient<IMemberVerificationsService, MemberVerificationsService>();

    services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

    services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
        })
        .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultScheme,
            _ => { });

    services.AddAuthorization(options =>
    {
        options.AddPolicy("Student", policy =>
        {
            policy.AuthenticationSchemes.Add(OpenIdConnectDefaults.AuthenticationScheme);
            policy.RequireAuthenticatedUser();
        });

        options.AddPolicy("ApiKey", policy =>
        {
            policy.AuthenticationSchemes.Add(ApiKeyAuthenticationOptions.DefaultScheme);
            policy.RequireAuthenticatedUser();
        });
    });

    services.AddControllersWithViews(options =>
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
        
        options.Filters.Add(new AuthorizeFilter(policy));
    });
    
    services.AddRazorPages().AddMicrosoftIdentityUI();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();