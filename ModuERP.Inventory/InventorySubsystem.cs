using MudBlazor;
using Microsoft.Extensions.DependencyInjection;
using ModuERP.Core.Interfaces;
using System.Runtime.CompilerServices;

public class InventorySubsystem : ISubsystem
{
    public string Name => "Inventory";
    public string Route => "/inventory";
    public string? Icon => Icons.Material.Outlined.Inventory;
    public int Order => 2;

    public void RegisterServices(IServiceCollection services)
    {
        //services.AddSingleton<IInventoryService, InventoryService>();
    }
}
