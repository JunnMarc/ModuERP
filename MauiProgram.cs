using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ModuERP.Data.Common.Db;
using ModuERP.Core.Extensions;
using ModuERP.Core.Loader;
using MudBlazor.Services;

namespace ModuERP.MainApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Load configuration
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // Get connection string
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Register EF Core DbContext
        builder.Services.AddDbContext<ModuERPDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register core services
        builder.Services.AddCoreServices();

        // Register MudBlazor
        builder.Services.AddMudServices();

        // Register modules via ModuleLoader
        var loggerFactory = LoggerFactory.Create(logging =>
        {
            logging.AddConsole();
            logging.AddDebug();
        });

        var logger = loggerFactory.CreateLogger<ModuleStartupInitializer>();
        var initializer = new ModuleStartupInitializer(logger, builder.Services);
        initializer.Initialize();

        return builder.Build();
    }
}
