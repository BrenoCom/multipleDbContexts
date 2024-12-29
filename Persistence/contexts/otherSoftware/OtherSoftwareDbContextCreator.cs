using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.contexts.mySoftware.Models;
using Persistence.contexts.otherSoftware;
using Persistence.contexts.otherSoftware.Models;

namespace Persistence.contexts.otherSoftware;

public class OtherSoftwareDbContextCreator : IOtherSoftwareDbContextCreator
{
    private readonly OtherSoftwareDbOptions _options;

    public OtherSoftwareDbContextCreator(IOptions<OtherSoftwareDbOptions> option)
    {
        _options = option.Value;
    }
    public OtherSoftwareDbContext CreateTenantContext(CompanyModel company, bool fatInv)
    {
        var conn = _options.ConnectionString;
        
        var serverERP = company.serverERP!;
        var baseERP = company.baseERP!;
        var codeERP = company.codeERP!;
        var yearERP = company.yearERP!;

        if (fatInv)
        {
            serverERP = company.serverERPIF!;
            baseERP = company.baseERPIF!;
            codeERP = company.codeERPIF!;
            yearERP = company.yearERPIF!;
        }

        conn = conn.Replace("{ServerERP}", serverERP).Replace("{BaseERP}", baseERP);

        var optionsBuilder = new DbContextOptionsBuilder<OtherSoftwareDbContext>();
        optionsBuilder
            .UseSqlServerMultiTenant(conn, null, new OtherSoftwareDbContextModelConfiguration(codeERP, yearERP).BuildModel)
            .LogTo(s => Console.WriteLine(s));

        var context = new OtherSoftwareDbContext(optionsBuilder.Options);

        return context;
    }
}