using Application.Features.Company;
using Application.Features.Invoice;
using Application.Features.Supplyer;
using Carter;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Persistence.contexts.mySoftware;
using Persistence.contexts.otherSoftware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MySoftwareDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("mySoftwareDB"));
});

builder.Services.Configure<OtherSoftwareDbOptions>(opt => {
    builder.Configuration.GetSection("OtherSoftwareDBOptions").Bind(opt);
});
builder.Services.AddScoped<IOtherSoftwareDbContextCreator, OtherSoftwareDbContextCreator>();


builder.Services.AddScoped<IInvoiceService, InvoiceSercice>();
builder.Services.AddScoped<ISupplyerService, SupplyerSercice>();
builder.Services.AddScoped<ICompanyService, CompanySercice>();

builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapCarter();

app.Run();

