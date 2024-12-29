using Microsoft.EntityFrameworkCore;
using Persistence.contexts.mySoftware;
using Persistence.contexts.otherSoftware;
using Persistence.contexts.otherSoftware.Models;

namespace Application.Features.Invoice;

public class InvoiceSercice : IInvoiceService
{
    private readonly MySoftwareDbContext _mySoftwareDbContext;
    private readonly IOtherSoftwareDbContextCreator _OSDbContextCreator;

    public InvoiceSercice(MySoftwareDbContext mySoftwareDbContext, IOtherSoftwareDbContextCreator otherSoftwareDbContextCreator)
    {
        _mySoftwareDbContext = mySoftwareDbContext;
        _OSDbContextCreator = otherSoftwareDbContextCreator;
    }

    public async Task<List<InvoiceOSModel>> List(int idCompany, bool isIF)
    {

        var company = await _mySoftwareDbContext.Company.FirstOrDefaultAsync(x => x.idCompany == idCompany);
        var lst = new List<InvoiceOSModel>();
        
        using (var osContext = _OSDbContextCreator.CreateTenantContext(company!, isIF))
        {
            lst = await osContext.Incoices.ToListAsync();
        }

        return lst;
    }
}