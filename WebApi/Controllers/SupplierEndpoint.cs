using Application.Features.Invoice;
using Application.Features.Supplier;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class SupplierEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/supplier", async (
            [FromQuery(Name = "companyId")] int idCompany
            ,[FromQuery(Name = "if")] bool isIF, ISupplierService supplierService) => {

            if(idCompany == 0)
                return Results.NotFound("No results");
            var lst = await supplierService.List(idCompany, isIF);

            return Results.Ok(lst);
        });
    }
}