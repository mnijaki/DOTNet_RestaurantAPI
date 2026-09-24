using NLog.Web;
using RestaurantAPI;
using RestaurantAPI.Entities;
using RestaurantAPI.Middleware;
using RestaurantAPI.Services;

// Create builder of the application (creation of Web Host).
var builder = WebApplication.CreateBuilder(args);

// Add NLog as logging provider.
builder.UseNLog();

// Ignore circular reference, e.g., Restaurant and Address entities (Restaurant references Address, and Address references Restaurant).
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

#region Inject services.

// Add services to the DI container.
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
// Register Swagger services that generate documentation (in JSON format) based on the OpenAPI specification.
builder.Services.AddSwaggerGen();

#endregion

// Build the application.
var app = builder.Build();

#region Seed entities with data.

// Scoped services in ASP.NET Core are typically created once per HTTP request.
// However, when we're at the application startup phase (before handling any requests), there's no active scope yet.
// `app.Services` gives you access to the root service provider.
// It's not recommended to resolve scoped services directly from the root provider (`app.Services`), as this can lead to memory leaks.
// The `CreateScope()` method creates a temporary scope that will properly manage the lifetime of any scoped services.
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();
    seeder.Seed();
}

#endregion

#region Configure middleware(HTTP request pipeline).

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Add middleware to handle errors.
app.UseMiddleware<ErrorHandlingMiddleware>();

// If a client uses HTTP in the address, he will be automatically redirected to the same address but with HTTPS.
app.UseHttpsRedirection();

// Enable routing (mapping between addresses and our controllers names and methods inside them).
app.UseRouting();

// Map controller endpoints.
app.MapControllers();

// Add Swagger middleware that listens to the '/swagger/v1/swagger.json' endpoint to serve the generated documentation.
// The document is created on the fly via the SwaggerGen service.
app.UseSwagger();
// Add Swagger UI middleware to render the interactive web documentation in the browser.
// SwaggerUI address: https://localhost:44320/swagger/index.html
app.UseSwaggerUI();

#endregion

// Start the application.
app.Run();