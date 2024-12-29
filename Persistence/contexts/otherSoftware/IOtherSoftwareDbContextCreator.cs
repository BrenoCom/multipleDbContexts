using Persistence.contexts.mySoftware.Models;

namespace Persistence.contexts.otherSoftware;

public interface IOtherSoftwareDbContextCreator
{
    public OtherSoftwareDbContext CreateTenantContext(CompanyModel company, bool fatInv);
}