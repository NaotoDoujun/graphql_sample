using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Subscriptions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Bff.Models;
namespace Bff.Services
{
  public class TimedHostedService : ITimedHostedService, IHostedService, IDisposable
  {
    private readonly IServiceScopeFactory _scopeFactory;
    private int executionCount = 0;
    private readonly ILogger<TimedHostedService> _logger;
    private Timer _timer;
    readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);

    public TimedHostedService(IServiceScopeFactory scopeFactory, ILogger<TimedHostedService> logger)
    {
      _scopeFactory = scopeFactory;
      _logger = logger;
    }

    public async Task<string> HeyAsync() => await Task.Run(() => "hey! yo feel good? :P");

    public Task StartAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Timed Hosted Service running.");

      _timer = new Timer(HandleTimerCallback, null, TimeSpan.Zero,
          TimeSpan.FromMilliseconds(1000));

      return Task.CompletedTask;
    }

    private async void HandleTimerCallback(object state)
    {
      using var scope = _scopeFactory.CreateScope();
      var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
      var eventSender = scope.ServiceProvider.GetRequiredService<ITopicEventSender>();
      await Semaphore.WaitAsync().ConfigureAwait(false);
      try
      {
        if (executionCount >= 100) executionCount = 0;
        var count = Interlocked.Increment(ref executionCount);
        var now = DateTime.Now;
        var counter = new Counter
        {
          Count = count,
          RecordTime = now
        };
        await dbContext.Counters.AddAsync(counter);
        await dbContext.SaveChangesAsync();
        await eventSender.SendAsync("ReturnedCounter", counter);
        _logger.LogInformation(
            "Timed Hosted Service is working. Count: {Count}", count);
      }
      finally
      {
        Semaphore.Release();
      }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Timed Hosted Service is stopping.");

      _timer?.Change(Timeout.Infinite, 0);

      return Task.CompletedTask;
    }

    public void Dispose()
    {
      _timer?.Dispose();
    }
  }
}