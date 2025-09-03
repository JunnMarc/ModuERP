using Microsoft.Extensions.DependencyInjection;
using ModuERP.Core.Interfaces;

namespace ModuERP.Hello;

public class HelloSubsystem : ISubsystem
{
    public string Name => "Hello Subsystem";
    public string Route => "/hello";

    public void RegisterServices(IServiceCollection services)
    {
        // No special DI for now
    }
}
