using Application.Features.Company;
using Application.Features.Invoice;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class CompanyEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/company", async (
            ICompanyService companyService) => {
            var lst = await companyService.List();

            return Results.Ok(lst);
        });
    }
}