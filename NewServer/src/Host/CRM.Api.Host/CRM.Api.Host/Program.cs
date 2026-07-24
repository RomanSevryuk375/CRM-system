using CRM.Billing.Application.Extensions;
using CRM.Billing.Infrastructure.Extensions;
using CRM.Shared.Infrastructure.Application.Extensions;
using CRM.Shared.Infrastructure.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGlobalSharedInfrastructure(builder.Configuration);
builder.Services.AddGlobalSharedApplication();

builder.Services.AddBillingInfrastructure(builder.Configuration);
builder.Services.AddBillingApplication();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
