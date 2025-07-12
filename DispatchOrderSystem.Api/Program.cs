using DispatchOrderSystem.Api.Middlewares;
using DispatchOrderSystem.Application;
using DispatchOrderSystem.Application.Behaviors;
using DispatchOrderSystem.Application.Validators;
using DispatchOrderSystem.Infrastructure.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add MediatR for CQRS pattern
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors?.Count > 0)
            .Select(e => new {
                Field = e.Key,
                Errors = e.Value?.Errors?.Select(err => err.ErrorMessage)
            });

        return new BadRequestObjectResult(new
        {
            message = "Validation failed.",
            details = errors
        });
    };
});

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

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderRequestValidator>();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

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
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
