using Persistence.contexts.otherSoftware.Models;

namespace Application.Features.Supplier;

public interface ISupplierService
{
    public Task<List<SupplierOSModel>> List(int idCompany, bool isIF);
    public Task<SupplierOSModel> Create(int idCompany, bool isIF, SupplierOSModel supplier);
    public Task<SupplierOSModel?> Update(int idCompany, bool isIF, SupplierOSModel supplier);
    public Task<int> Delete(int idCompany, bool isIF, string supplierCode);
}