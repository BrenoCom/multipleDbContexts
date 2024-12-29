using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.contexts.otherSoftware;

public static class MultiTenancyDbContextOptionsExtensions
{
    public static DbContextOptionsBuilder UseSqlServerMultiTenant(this DbContextOptionsBuilder optionsBuilder, string connectionString, string? schemaName, Func<string?, IModel> buildModel)
    {
        optionsBuilder
            .UseSqlServer(connectionString, o => o.MigrationsHistoryTable("__EFMigrationsHistory", !string.IsNullOrEmpty(schemaName) ? schemaName : null))
            .UseModel(buildModel(schemaName))
            .ReplaceService<IMigrationsSqlGenerator, MultitenancySqlServerMigrationsSqlGenerator>();

        // Ignore pending model changes warning
        if (!string.IsNullOrEmpty(schemaName))
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));

        return optionsBuilder;
    }
}