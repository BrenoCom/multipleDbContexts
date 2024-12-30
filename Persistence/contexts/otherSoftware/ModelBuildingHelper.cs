using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.contexts.otherSoftware;

public static class ModelBuildingHelper
{
    private static readonly object _lock = new object();
    private static ConventionSet? _sqlServerConventionSet = null;

    public static ConventionSet GetSqlServerConventionSet()
    {
        if (_sqlServerConventionSet is null)
        {
            lock (_lock)
            {
                if (_sqlServerConventionSet is null)
                {
                    var serviceProvider = new ServiceCollection()
                        .AddSqlServer<DbContext>("no-connection")
                        .BuildServiceProvider();
                    var context = serviceProvider
                        .GetRequiredService<DbContext>();
                    var conventionSetBuilder = context.GetInfrastructure()
                        .GetRequiredService<IProviderConventionSetBuilder>();
                    _sqlServerConventionSet = conventionSetBuilder.CreateConventionSet();
                }
            }
        }

        return _sqlServerConventionSet;
    }
}