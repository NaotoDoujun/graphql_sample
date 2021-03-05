using System;
using System.Threading.Tasks;
using System.Linq;
using HotChocolate;
using Bff.Services;
namespace Bff.Models
{
  public class Query
  {
    private readonly ITimedHostedService _service;
    public Query(ITimedHostedService service)
    {
      _service = service;
    }

    public IQueryable<Counter> GetCounters([Service] ApplicationDbContext context)
    {
      return context.Counters;
    }

    public Task<string> GetHey() => _service.HeyAsync();
  }
}