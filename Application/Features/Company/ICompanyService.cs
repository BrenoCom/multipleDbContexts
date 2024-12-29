using Persistence.contexts.mySoftware.Models;

namespace Application.Features.Company;

public interface ICompanyService
{
    public Task<List<CompanyModel>> List();
}