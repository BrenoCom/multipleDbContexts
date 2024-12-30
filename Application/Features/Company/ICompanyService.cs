using Persistence.contexts.mySoftware.Models;

namespace Application.Features.Company;

public interface ICompanyService
{
    public Task<List<CompanyModel>> List();
    public Task<CompanyModel> Create(CompanyModel company);
    public Task<CompanyModel?> Update(CompanyModel company);
    public Task<int> Delete(int idCompany);
}