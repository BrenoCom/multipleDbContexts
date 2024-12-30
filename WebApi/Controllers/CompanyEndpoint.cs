using Application.Features.Company;
using Carter;
using Microsoft.AspNetCore.Mvc;
using Persistence.contexts.mySoftware.Models;

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

        app.MapPost("api/company", async (
            ICompanyService companyService,
            [FromBody] CompanyModel company) => {

            var newCompany = await companyService.Create(company);

            return Results.Ok(newCompany);
        });

        app.MapPut("api/company", async (
            ICompanyService companyService,
            [FromBody] CompanyModel company) => {

            var updatedCompany = await companyService.Update(company);
            if (updatedCompany is null) return Results.NotFound("Company not found");

            return Results.Ok(updatedCompany);
        });

        app.MapDelete("api/company/{id}", async (
            ICompanyService companyService,
            [FromRoute] int id) => {

            var count = await companyService.Delete(id);
            if (count < 1) return Results.NotFound("Company not found");

            return Results.NoContent();
        });
    }
}