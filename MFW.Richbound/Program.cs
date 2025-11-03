using System.Diagnostics.CodeAnalysis;
using MFW.Richbound.Factories;
using MFW.Richbound.Factories.Interfaces;
using MFW.Richbound.Infrastructure;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Presentation;
using MFW.Richbound.Providers;
using MFW.Richbound.Providers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MFW.Richbound;

/// <summary>
/// Represents the primary entry point for running the application.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Program
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    public static void Main()
    {
        var serviceProvider = ConfigureServices();
        var runner = serviceProvider.GetRequiredService<Runner>();

        runner.Run();
    }

    /// <summary>
    /// Configures and builds a service provider with registered dependency injection services for the application.
    /// </summary>
    /// <returns>A <see cref="ServiceProvider"/> instance containing the configured services.</returns>
    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Register core framework services.
        services.AddSingleton(TimeProvider.System);

        // Register pre-configured services using direct instance registration.
        services.AddSingleton<IAssemblyVersionProvider>(new AssemblyVersionProvider(typeof(Program).Assembly));

        // Register services.
        services.AddSingleton<IConsoleLogger, ConsoleLogger>();
        services.AddTransient<IConsoleWrapper, ConsoleWrapper>();
        services.AddTransient<IPromptFactory, PromptFactory>();

        // Register runner service to manage application loop.
        services.AddTransient<Runner>();

        // Register prompts.
        services.AddTransient<MainMenu>();

        return services.BuildServiceProvider();
    }
}
