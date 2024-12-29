using Application.Features.Invoice;
using Carter;
using Microsoft.AspNetCore.Mvc;

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
    }
}