using Authentication.Api.Extensions;
using Authentication.Application.Services;
using Authentication.Infrastructure.Services;
using Shared.Abstractions.Commands;
using Shared.Abstractions.Queries;
using Shared.Infrastructure.Commands;
using Shared.Infrastructure.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICommandSender, ServiceProviderCommandSender>();
builder.Services.AddScoped<IQuerySender, ServiceProviderQuerySender>();
builder.Services.AddSingleton<IPasswordHasherService, BCryptPasswordHasherService>();
builder.Services.AddSingleton<IJwtProviderService, MicrosoftIdentityModelJwtProviderService>();
builder.Services.AddJwtOptions(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();