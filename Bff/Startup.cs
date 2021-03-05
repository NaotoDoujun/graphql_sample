using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Bff.Models;
using Bff.Services;
namespace Bff
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=bff.db"));
      services.AddCors();
      services.AddGraphQLServer()
        .AddQueryType<Query>()
        .AddSubscriptionType<Subscription>()
        .AddInMemorySubscriptions();
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
      builder.RegisterModule(new ServiceModule());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseWebSockets();
      app.UseRouting();
      app.UseEndpoints(x => x.MapGraphQL());
      app.UseCors(builder => builder.WithOrigins("http://localhost:3000"));

    }
  }
}