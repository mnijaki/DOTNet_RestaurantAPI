using RestaurantAPI;
using RestaurantAPI.Entities;
using RestaurantAPI.Services;

// Create our application (creation of Web Host).
var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container (similar to how it was previously done in Startup.ConfigureServices()).
// Configuration. -> builder.configuration.
// could not find AuthenticationSettings class
// remember to fix lambda
// instead of this.Get... -> ma byc Assembly.GetExecutingAssembly()
//builder.Configuration.GetSection("Authentication").Bind(builder.Configuration);
// services -> builder.Services.
// Ignore circular reference, e.g., Restaurant and Address entities (Restaurant references Address, and Address references Restaurant).
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
// Add concrete DbContext and Seeder class to seed entities with data later on.
builder.Services.AddDbContext<RestaurantDbContext>();
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

// Build our application.
var app = builder.Build();

// Seed entities with data.
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


// Configure the HTTP request pipeline.
// seeder -> var scope = app.Services.CreateScope(); var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();
// env. -> app.Environment.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
// If a client uses address HTTP, he will be automatically redirected to the same address but with HTTPS.
app.UseHttpsRedirection();
// Enable routing (mapping between addresses and our controllers names and methods inside them).
app.UseRouting();
// Mapping to controller endpoints.
app.MapControllers();
// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();
// });
// Start the application.
app.Run();