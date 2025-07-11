using DispatchOrderSystem.Infrastructure.Data;
using DispatchOrderSystem.Infrastructure.DependencyInjection;
using System.Reflection;
using MediatR;
using DispatchOrderSystem.Application;


var builder = WebApplication.CreateBuilder(args);

// Add MediatR for CQRS pattern
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
});

// Add services to the container.
builder.Services.AddControllers();

// Add services to the Infrastructure (DbContext, repos, services)
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

// Configure middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Dispatch Order System Api";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "DispatchOrderSystem API V1");
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();
