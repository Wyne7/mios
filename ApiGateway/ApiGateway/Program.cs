using ApiGateway;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Bridge: Configure Services via Startup class
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

// Add Ocelot Config
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// --- PIPELINE ORDER IS KEY ---

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// 1. Force Routing to initialize here
app.UseRouting();

// 2. Map your "Hello" endpoint BEFORE Ocelot starts
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", () => "Hello ApiGateway!");
});

// 3. Bridge: Call Startup.Configure for Ocelot
startup.Configure(app, app.Environment);

app.Run();