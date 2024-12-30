using Persistence.contexts.otherSoftware.Models;

namespace Application.Features.Invoice;

public interface IInvoiceService
{
    public Task<List<InvoiceOSModel>> List(int idCompany, bool isIF);
    public Task<InvoiceOSModel> Create(int idCompany, bool isIF, InvoiceOSModel invoice);
    public Task<InvoiceOSModel?> Update(int idCompany, bool isIF, InvoiceOSModel invoice);
    public Task<int> Delete(int idCompany, bool isIF, string supplierCode, string invoiceNumber);
}