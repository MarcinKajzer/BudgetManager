using BudgetManager.API;
using BudgetManager.API.ServicesExtensions;
using BudgetManager.Application;
using BudgetManager.Application.Common;
using BudgetManager.Application.Interfaces;
using BudgetManager.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Log.Logger = new LoggerConfiguration()
//        .ReadFrom.Configuration(builder.Configuration)
//        .CreateLogger();
//builder.Host.UseSerilog();

builder.Host.UseSerilog((_, _, config) => config.ReadFrom.Configuration(builder.Configuration));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IIdStorage, IdStorage>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policyBuilder =>
{
    policyBuilder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .WithExposedHeaders("Location")
        .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Enumerable.Empty<string>().ToArray());
});

app.UseAuthorization();

app.MapControllers();

app.Run();
