using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot(Configuration);
        }

        // Must be async void or async Task for Ocelot
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // IMPORTANT: Do NOT call app.UseRouting() here. 
            // It was already called in Program.cs

            await app.UseOcelot();
        }
    }
}