using Api.Extensions;
using Api.OptionsSetup;
using Application;
using Application.Abstractions.Authentication;
using Infrastructure;
using Infrastructure.Authentication;
using Infrastructure.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetRequiredSection(nameof(EmailSettings)));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
builder.Services.AddTransient<IJwtProvider, JwtProvider>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.WriteTo.Console();
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.ApplyMigration();
//app.SeedData();
app.SeedDataUser();

app.UseAuthentication();
app.UseAuthorization();
app.UseCustomExceptionHandler();
app.MapControllers();

app.Run();

