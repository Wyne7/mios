
using mios.management.API;

var builder = WebApplication.CreateBuilder(args);

// Load the Ocelot configuration file
//builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Manually wire the Startup class
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Run the middleware configuration
startup.Configure(app, app.Environment);
app.MapGet("/", () => "Hello Management Service!");


app.Run();