using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ModuERP.Core.Extensions;
using ModuERP.Core.Interfaces;
using ModuERP.Core.Loader;
using ModuERP.Core.Services;
using ModuERP.Data.Common.Db;
using MudBlazor.Services;

namespace ModuERP;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMudServices();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        // Load appsettings.json
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // ✅ Use SQL Server (SSMS)
        builder.Services.AddDbContext<ModuERPDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Auth + session
        builder.Services.AddSingleton<ISessionStorage, MauiSessionStorage>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddCoreServices();

        var app = builder.Build();

        // ✅ Ensure DB exists & apply migrations
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ModuERPDbContext>();
            db.Database.Migrate(); // applies migrations automatically
        }



        return app;
    }
}
