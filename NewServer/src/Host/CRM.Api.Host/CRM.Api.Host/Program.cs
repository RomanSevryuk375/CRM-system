using CRM.Billing.Api;
using CRM.Shared.Infrastructure.Application.Extensions;
using CRM.Shared.Infrastructure.Configuration;
using CRM.Shared.Infrastructure.Extensions;

EnvLoader.Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGlobalSharedInfrastructure(builder.Configuration);
builder.Services.AddGlobalSharedApplication();

builder.Services.AddBillingModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
