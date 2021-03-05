using System.Threading.Tasks;
namespace Bff.Services
{
  public interface ITimedHostedService
  {
    public Task<string> HeyAsync();
  }
}