using Microsoft.EntityFrameworkCore;
using Persistence.contexts.mySoftware;
using Persistence.contexts.otherSoftware;
using Persistence.contexts.otherSoftware.Models;

namespace Application.Features.Supplyer;

public class SupplyerSercice : ISupplyerService
{
    private readonly MySoftwareDbContext _mySoftwareDbContext;
    private readonly IOtherSoftwareDbContextCreator _OSDbContextCreator;

    public SupplyerSercice(IOtherSoftwareDbContextCreator oSDbContextCreator, MySoftwareDbContext mySoftwareDbContext)
    {
        _OSDbContextCreator = oSDbContextCreator;
        _mySoftwareDbContext = mySoftwareDbContext;
    }

    public async Task<List<SupplyerOSModel>> List(int idCompany, bool isIF)
    {
        var company = await _mySoftwareDbContext.Company.FirstOrDefaultAsync(x => x.idCompany == idCompany);
        var lst = new List<SupplyerOSModel>();
        
        using (var osContext = _OSDbContextCreator.CreateTenantContext(company!, isIF))
        {
            lst = await osContext.Supplyers.ToListAsync();
        }

        return lst;
    }
}