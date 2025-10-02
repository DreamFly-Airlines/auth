using Authentication.Api.Extensions;
using Authentication.Application.Commands;
using Authentication.Application.Queries;
using Authentication.Application.Services;
using Authentication.Domain.Repositories;
using Authentication.Infrastructure.Repositories;
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
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddJwtOptions(builder.Configuration);
builder.Services.AddCommandHandlers(typeof(RegisterCommandHandler).Assembly);
builder.Services.AddQueryHandlers(typeof(LogInQueryHandler).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();