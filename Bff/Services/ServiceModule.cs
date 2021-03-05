using Autofac;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace Bff.Services
{
  public class ServiceModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.Register(c => new TimedHostedService(
        c.Resolve<IServiceScopeFactory>(),
        c.Resolve<ILogger<TimedHostedService>>()))
          .As<IHostedService>()
          .As<ITimedHostedService>()
          .InstancePerLifetimeScope();
    }
  }
}