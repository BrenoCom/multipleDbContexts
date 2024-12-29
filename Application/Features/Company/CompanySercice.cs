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

    public async Task<List<CompanyModel>> List()
    {
        var lst = await _mySoftwareDbContext.Company.ToListAsync();

        return lst;
    }
}