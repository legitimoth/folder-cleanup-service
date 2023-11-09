using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FolderCleanupService;
class Program
{
    static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var app = serviceProvider.GetRequiredService<IFolderCleanupService>();
        await app.KeepRecentFoldersAsync();
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.AddSingleton<IConfiguration>(configuration)
        .AddScoped<ILogger, Logger>()
        .AddScoped<IFolderCleanupService, FolderCleanupService>()
        .BuildServiceProvider();
    }
}