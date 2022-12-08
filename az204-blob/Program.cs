using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Azure Blob Storage exercise\n");

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddScoped<IStorageService, StorageService>())
    .Build();

await Run(host.Services);

static async Task Run(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    var provider = serviceScope.ServiceProvider;
    var storageService = provider.GetRequiredService<IStorageService>();

    var blobServiceClient = storageService.CreateConnection();
    var blobContainerClient = await storageService.CreateContainer(blobServiceClient);

    await storageService.SetPolicy(Guid.NewGuid().ToString(), 1, blobContainerClient);
    await storageService.Upload(blobContainerClient);
    await storageService.Load(blobContainerClient);
    await storageService.Download(blobContainerClient);
    await storageService.Delete(blobContainerClient);
    await storageService.GenerateUrlSas(blobContainerClient);
}

Console.WriteLine("Press enter to exit the sample application.");
Console.ReadLine();