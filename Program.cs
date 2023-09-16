using Microsoft.EntityFrameworkCore;
using ProductsChona.Datos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Use SQLite connection string
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
}); 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// los scopes se borran luego de hacer lo suyo. automaticamente se limpian/destruyen. 
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try{
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

} catch (Exception ex) {
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex,"An error occur during migration");
    }
// In summary, this code creates a scope for dependency injection, retrieves the ApplicationDbContext and a logger from the
// service provider, and attempts to apply pending database migrations. If an exception occurs during migration, it logs the 
//error using the logger.
// This is a best practice for ensuring that your database schema is kept in sync with changes to your application's data model 
//as you develop and deploy updates.
app.Run();
