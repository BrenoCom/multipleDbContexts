using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

    public async Task<InvoiceOSModel> Create(int idCompany, bool isIF, InvoiceOSModel invoice)
    {
        var company = await _mySoftwareDbContext.Company.FirstOrDefaultAsync(x => x.idCompany == idCompany);
        var createdValue = (EntityEntry<InvoiceOSModel>?) null;
        using (var osContext = _OSDbContextCreator.CreateTenantContext(company!, isIF))
        {
            createdValue = osContext.Incoices.Add(invoice);
            
            await osContext.SaveChangesAsync();
        }

        return createdValue.Entity;
    }

    public async Task<int> Delete(int idCompany, bool isIF, string supplierCode, string invoiceNumber)
    {
        var company = await _mySoftwareDbContext.Company.FirstOrDefaultAsync(x => x.idCompany == idCompany);
        var count = 0;
        using (var osContext = _OSDbContextCreator.CreateTenantContext(company!, isIF))
        {
            count = await osContext.Incoices
                .Where(x => x.SupplierCode == supplierCode && x.InvoiceNo == invoiceNumber)
                .ExecuteDeleteAsync();
        }

        return count;
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

    public async Task<InvoiceOSModel?> Update(int idCompany, bool isIF, InvoiceOSModel invoice)
    {
        var company = await _mySoftwareDbContext.Company.FirstOrDefaultAsync(x => x.idCompany == idCompany);
        var updatedValue = (InvoiceOSModel?) null;
        using (var osContext = _OSDbContextCreator.CreateTenantContext(company!, isIF))
        {
            updatedValue = osContext.Incoices
                .FirstOrDefault(x => x.SupplierCode == invoice.SupplierCode && x.InvoiceNo == invoice.InvoiceNo);
            
            if (updatedValue is null) return null;

            updatedValue.TransactioNo = invoice.TransactioNo;
            updatedValue.InvoiceDate = invoice.InvoiceDate;
            updatedValue.BookEntrDate = invoice.BookEntrDate;
            updatedValue.DueDate = invoice.DueDate;

            await osContext.SaveChangesAsync();
        }

        return updatedValue;
    }
}