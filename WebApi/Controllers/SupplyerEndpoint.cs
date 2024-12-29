using Application.Features.Invoice;
using Application.Features.Supplyer;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class SupplyerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/supplyer", async (
            [FromQuery(Name = "companyId")] int idCompany
            ,[FromQuery(Name = "if")] bool isIF, ISupplyerService supplyerService) => {

            if(idCompany == 0)
                return Results.NotFound("No results");
            var lst = await supplyerService.List(idCompany, isIF);

            return Results.Ok(lst);
        });
    }
}