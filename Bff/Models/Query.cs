using System.Threading.Tasks;
using System.Linq;
using HotChocolate;
using Bff.Services;
namespace Bff.Models
{
  public class Query
  {
    public IQueryable<Counter> GetCounters([Service] ApplicationDbContext context)
    {
      return context.Counters.AsQueryable();
    }

    public IQueryable<Counter> GetCounter(int id, [Service] ApplicationDbContext context)
    {
      return context.Counters.Where(counter => counter.Id == id);
    }

    public Task<string> GetHey([Service] ITimedHostedService service) => service.HeyAsync();
  }
}