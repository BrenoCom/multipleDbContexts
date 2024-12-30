using Application.Features.Invoice;
using Application.Features.Supplier;
using Carter;
using Microsoft.AspNetCore.Mvc;
using Persistence.contexts.otherSoftware.Models;

namespace WebApi.Controllers;

public class SupplierEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/supplier", async (
            [FromQuery(Name = "companyId")] int idCompany,
            [FromQuery(Name = "if")] bool isIF, 
            ISupplierService supplierService) => {

            if(idCompany == 0)
                return Results.NotFound("No results");
            var lst = await supplierService.List(idCompany, isIF);

            return Results.Ok(lst);
        });

        app.MapPost("api/supplier", async (
            [FromQuery(Name = "companyId")] int idCompany,
            [FromQuery(Name = "if")] bool isIF,
            ISupplierService supplierService,
            [FromBody] SupplierOSModel supplier) => {

            var newCompany = await supplierService.Create(idCompany, isIF, supplier);

            return Results.Ok(newCompany);
        });

        app.MapPut("api/supplier", async (
            [FromQuery(Name = "companyId")] int idCompany,
            [FromQuery(Name = "if")] bool isIF,
            ISupplierService supplierService,
            [FromBody] SupplierOSModel supplier) => {

            var updatedCompany = await supplierService.Update(idCompany, isIF, supplier);
            if (updatedCompany is null) return Results.NotFound("Supplier not found");

            return Results.Ok(updatedCompany);
        });

        app.MapDelete("api/supplier/{supplierCode}", async (
            [FromQuery(Name = "companyId")] int idCompany,
            [FromQuery(Name = "if")] bool isIF,
            ISupplierService supplierService,
            [FromRoute] string supplierCode) => {

            var count = await supplierService.Delete(idCompany, isIF, supplierCode);
            if (count < 1) return Results.NotFound("Supplier not found");

            return Results.NoContent();
        });
    }
}