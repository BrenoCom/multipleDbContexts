using Microsoft.EntityFrameworkCore;
using Persistence.contexts.mySoftware;
using Persistence.contexts.mySoftware.Models;

namespace Application.Features.Company;

public class CompanySercice : ICompanyService
{
    private readonly MySoftwareDbContext _mySoftwareDbContext;

    public CompanySercice(MySoftwareDbContext mySoftwareDbContext)
    {
        _mySoftwareDbContext = mySoftwareDbContext;
    }

    public async Task<CompanyModel> Create(CompanyModel company)
    {
        if(!company.hasIF)
        {
            company.serverERPIF = "";
            company.baseERPIF = "";
            company.codeERPIF = "";
            company.yearERPIF = "";
        }
        var createdValue = _mySoftwareDbContext.Company.Add(company);

        await _mySoftwareDbContext.SaveChangesAsync();

        return createdValue.Entity;
    }

    public async Task<int> Delete(int idCompany)
    {
        var count = await _mySoftwareDbContext.Company.Where(x => x.idCompany == idCompany).ExecuteDeleteAsync();
        _mySoftwareDbContext.SaveChanges();
        return count;
    }

    public async Task<List<CompanyModel>> List()
    {
        var lst = await _mySoftwareDbContext.Company.ToListAsync();

        return lst;
    }

    public async Task<CompanyModel?> Update(CompanyModel company)
    {
        var updatedValue = _mySoftwareDbContext.Company
            .FirstOrDefault(x => x.idCompany == company.idCompany);
        
        if (updatedValue == null) return null;

        updatedValue.serverERP = company.serverERP;
        updatedValue.baseERP = company.baseERP;
        updatedValue.codeERP = company.codeERP;
        updatedValue.yearERP = company.yearERP;

        updatedValue.serverERPIF = "";
        updatedValue.baseERPIF = "";
        updatedValue.codeERPIF = "";
        updatedValue.yearERPIF = "";
        
        updatedValue.hasIF = company.hasIF;
        if (company.hasIF)
        {
            updatedValue.serverERPIF = company.serverERPIF;
            updatedValue.baseERPIF = company.baseERPIF;
            updatedValue.codeERPIF = company.codeERPIF;
            updatedValue.yearERPIF = company.yearERPIF;
        }
        

        await _mySoftwareDbContext.SaveChangesAsync();

        return updatedValue;
    }
}