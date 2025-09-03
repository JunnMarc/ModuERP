using Microsoft.Extensions.DependencyInjection;
using ModuERP.Core.Interfaces;
using MudBlazor;

namespace ModuERP;

public class DashboardSubsystem : ISubsystem
{
    public string Name => "Dashboard";
    public string Route => "/dashboard";
    public string? Icon => Icons.Material.Filled.Dashboard;
    public int Order => 1;

    public void RegisterServices(IServiceCollection services)
    {
        // Nothing for now
    }
}
