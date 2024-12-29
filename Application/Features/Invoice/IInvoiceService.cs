using Persistence.contexts.otherSoftware.Models;

namespace Application.Features.Invoice;

public interface IInvoiceService
{
    public Task<List<InvoiceOSModel>> List(int idCompany, bool isIF);
}