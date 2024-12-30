using Persistence.contexts.otherSoftware.Models;

namespace Application.Features.Supplier;

public interface ISupplierService
{
    public Task<List<SupplierOSModel>> List(int idCompany, bool isIF);
}