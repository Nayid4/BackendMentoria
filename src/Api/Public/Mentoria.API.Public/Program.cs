using Mentoria.Shared.Setup.API;

WebApplication app = DefaultMentoriaWebApplication.Create(args, webApplicationBuilder =>
{
    webApplicationBuilder.Services.AddReverseProxy()
        .LoadFromConfig(webApplicationBuilder.Configuration.GetSection("ReverseProxy"));
    webApplicationBuilder.Services.AddServiceBusIntegrationPublisher(webApplicationBuilder.Configuration);
});

app.MapReverseProxy();

DefaultMentoriaWebApplication.Run(app);
