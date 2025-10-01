using Shared.Abstractions.Commands;
using Shared.Infrastructure.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICommandSender, ServiceProviderCommandSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();