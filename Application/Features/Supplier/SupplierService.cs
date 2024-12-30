using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

    public async Task<SupplierOSModel> Create(int idCompany, bool isIF, SupplierOSModel supplier)
    {
        var company = await _mySoftwareDbContext.Company.FirstOrDefaultAsync(x => x.idCompany == idCompany);
        var createdValue = (EntityEntry<SupplierOSModel>?) null;
        using (var osContext = _OSDbContextCreator.CreateTenantContext(company!, isIF))
        {

            var lastSupplyerCode = osContext.Suppliers.Max(x => x.SupplierCode);
            if (lastSupplyerCode is null)
            {
                lastSupplyerCode = "0000000000";
            }
            var supplyerCodeCount = int.Parse(lastSupplyerCode) + 1;

            
            var newCode = ("0000000000" + supplyerCodeCount);
            supplier.SupplierCode = Microsoft.VisualBasic.Strings.Right(newCode, 10);
            createdValue = osContext.Suppliers.Add(supplier);
            
            await osContext.SaveChangesAsync();
        }

        return createdValue.Entity;
    }

    public async Task<int> Delete(int idCompany, bool isIF, string supplierCode)
    {
        var company = await _mySoftwareDbContext.Company.FirstOrDefaultAsync(x => x.idCompany == idCompany);
        var count = 0;
        using (var osContext = _OSDbContextCreator.CreateTenantContext(company!, isIF))
        {
            count = await osContext.Suppliers
                .Where(x => x.SupplierCode == supplierCode)
                .ExecuteDeleteAsync();
        }

        return count;
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

    public async Task<SupplierOSModel?> Update(int idCompany, bool isIF, SupplierOSModel supplier)
    {
        var company = await _mySoftwareDbContext.Company.FirstOrDefaultAsync(x => x.idCompany == idCompany);
        var updatedValue = (SupplierOSModel?) null;
        using (var osContext = _OSDbContextCreator.CreateTenantContext(company!, isIF))
        {
            updatedValue = osContext.Suppliers
                .FirstOrDefault(x => x.SupplierCode == supplier.SupplierCode);
            
            if (updatedValue is null) return null;

            updatedValue.SupplierName = supplier.SupplierName;
            updatedValue.AddressLine1 = supplier.AddressLine1;
            updatedValue.AddressLine2 = supplier.AddressLine2;
            updatedValue.AddressLine3 = supplier.AddressLine3;

            await osContext.SaveChangesAsync();
        }

        return updatedValue;
    }
}