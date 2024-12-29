using Persistence.contexts.otherSoftware.Models;

namespace Application.Features.Supplyer;

public interface ISupplyerService
{
    public Task<List<SupplyerOSModel>> List(int idCompany, bool isIF);
}