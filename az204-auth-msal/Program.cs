using az204_msal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddScoped<IAuthenticateService, AuthenticateService>();
        services.AddScoped<IMeService, MeService>();
    })
    .Build();

using IServiceScope serviceScope = host.Services.CreateScope();
var provider = serviceScope.ServiceProvider;
var authenticateService = provider.GetRequiredService<IAuthenticateService>();
var meService = provider.GetRequiredService<IMeService>();

var accessToken = await authenticateService.Authenticate();
await meService.ReadMe(accessToken);

host.Run();

Console.WriteLine("Press enter to exit the sample application.");
Console.ReadLine();