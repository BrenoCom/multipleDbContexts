using Application.Features.Invoice;
using Carter;
using Microsoft.AspNetCore.Mvc;
using Persistence.contexts.otherSoftware.Models;

namespace WebApi.Controllers;

public class InvoiceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/invoice", async (
            [FromQuery(Name = "companyId")] int idCompany
            ,[FromQuery(Name = "if")] bool isIF, IInvoiceService invoiceService) => {

            if(idCompany == 0)
                return Results.NotFound("No results");
            var lst = await invoiceService.List(idCompany, isIF);

            return Results.Ok(lst);
        });

        app.MapPost("api/invoice", async (
            [FromQuery(Name = "companyId")] int idCompany,
            [FromQuery(Name = "if")] bool isIF,
            IInvoiceService invoiceService,
            [FromBody] InvoiceOSModel invoice) => {

            var newCompany = await invoiceService.Create(idCompany, isIF, invoice);

            return Results.Ok(newCompany);
        });

        app.MapPut("api/invoice", async (
            [FromQuery(Name = "companyId")] int idCompany,
            [FromQuery(Name = "if")] bool isIF,
            IInvoiceService invoiceService,
            [FromBody] InvoiceOSModel invoice) => {

            var updatedCompany = await invoiceService.Update(idCompany, isIF, invoice);
            if (updatedCompany is null) return Results.NotFound("Invoice not found");

            return Results.Ok(updatedCompany);
        });

        app.MapDelete("api/invoice/{supplierCode}/{invoiceNumber}", async (
            [FromQuery(Name = "companyId")] int idCompany,
            [FromQuery(Name = "if")] bool isIF,
            IInvoiceService invoiceService,
            [FromRoute] string supplierCode,
            [FromRoute] string invoiceNumber) => {

            var count = await invoiceService.Delete(idCompany, isIF, supplierCode, invoiceNumber);
            if (count < 1) return Results.NotFound("Invoice not found");

            return Results.NoContent();
        });
    }
}