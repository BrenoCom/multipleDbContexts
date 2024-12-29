using Microsoft.EntityFrameworkCore;
using Persistence.contexts.mySoftware;
using Persistence.contexts.otherSoftware;
using Persistence.contexts.otherSoftware.Models;

namespace Application.Features.Supplier;

public class SupplierSercice : ISupplierService
{
    private readonly MySoftwareDbContext _mySoftwareDbContext;
    private readonly IOtherSoftwareDbContextCreator _OSDbContextCreator;

    public SupplierSercice(IOtherSoftwareDbContextCreator oSDbContextCreator, MySoftwareDbContext mySoftwareDbContext)
    {
        _OSDbContextCreator = oSDbContextCreator;
        _mySoftwareDbContext = mySoftwareDbContext;
    }

    public async Task<List<SupplierOSModel>> List(int idCompany, bool isIF)
    {
        var company = await _mySoftwareDbContext.Company.FirstOrDefaultAsync(x => x.idCompany == idCompany);
        var lst = new List<SupplierOSModel>();
        
        using (var osContext = _OSDbContextCreator.CreateTenantContext(company!, isIF))
        {
            lst = await osContext.Suppliers.ToListAsync();
        }

        return lst;
    }
}