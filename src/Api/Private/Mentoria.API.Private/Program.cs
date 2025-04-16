using Mentoria.Shared.Setup.API;
using Mentoria.Shared.Setup.API.Key;
using Mentoria.Shared.Setup.API.RateLimiting;

WebApplication app = DefaultMentoriaWebApplication.Create(args, webappBuilder =>
{
    webappBuilder.Services.AddReverseProxy()
        .LoadFromConfig(webappBuilder.Configuration.GetSection("ReverseProxy"));

    webappBuilder.Services.AddApiToken(webappBuilder.Configuration);
});

app.UseApiTokenMiddleware();
app.UseRateLimiter();
app.MapGet("/", () => "Hello World!");
app.MapGet("/rate-limiting-test", () =>
{
    return "Hello World!";
}).RequireRateLimiting(new MentoriaRateLimiterPolicy());

app.MapReverseProxy();

DefaultMentoriaWebApplication.Run(app);