using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// ===============================================
// Add Services to the DI Container
// ===============================================

// Add Controllers
builder.Services.AddControllers();

//Add Swagger / OpenAPI documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title="Task Management API",
        Version="v1",
        Description="A REST API for managing tasks with CRUD operations"
    });
});

//Add Database Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Server=.;Database=TaskDb;Trusted_Connection=True;TrustServerCertificate=True;";

builder.Services.AddDbContext<TaskDbContext>(options => options.UseSqlServer(connectionString));

//Add CORS policy (allows frontend to call this API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", corsBuilder =>
    {
        corsBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

//Add Logging
builder.Services.AddLogging();

//===============================================
//Build Application
//===============================================   

var app = builder.Build();
//===============================================
// Configure the HTTP Request Pipeline
//===============================================

if(app.Environment.IsDevelopment())
{
    // Enable Swagger UI in development environment
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API v1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

//Redirect HTTP to HTTPS
app.UseHttpsRedirection();

//Enable CORS
app.UseCors("AllowAll");

//Enable authentication and authorization (if added later)
app.UseAuthentication();
app.UseAuthorization();

//Map controller routes
app.MapControllers();

//===============================================
//Run the application
//===============================================

app.Run();