using az204_msal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddScoped<IMeService, MeService>();
    })
    .Build();

using IServiceScope serviceScope = host.Services.CreateScope();
var provider = serviceScope.ServiceProvider;
var meService = provider.GetRequiredService<IMeService>();

await CriarMenu();

host.Run();

async Task CriarMenu()
{
    Console.WriteLine("Digite a opção desejada:");
    Console.WriteLine("1- Provider delegate MSAL:");
    Console.WriteLine("2- Interactive Browser:");
    Console.WriteLine("3- Usuário/Senha:");
    Console.WriteLine("4- Device Code:");
    Console.WriteLine("5- Client Secret:");
    Console.WriteLine("6- Key Vault:");
    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            await meService.ReadDelegateAuthenticationProvider();
            break;
        case "2":
            await meService.ReadInteractiveBrowser();
            break;
        case "3":
            await meService.ReadUsernamePassword();
            break;
        case "4":
            await meService.ReadDeviceCode();
            break;
        case "5":
            await meService.ReadClientSecret();
            break;
        case "6":
            await meService.ReadKeyVault();
            break;
    }

    Console.WriteLine("");
    await CriarMenu();
}