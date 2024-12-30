using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.contexts.otherSoftware.Models;

namespace Persistence.contexts.otherSoftware;

public class OtherSoftwareDbContextModelConfiguration
{
    public readonly ConcurrentDictionary<string, IModel> ModelCache = new();
    private string _companyCode = string.Empty;
    private string _companyYear = string.Empty;

    public OtherSoftwareDbContextModelConfiguration(string companyCode, string companyYear)
    {
        _companyCode = companyCode;
        _companyYear = companyYear;
    }

    public ModelBuilder CreateModelBuilder()
    {
        // Create the ModelBuilder with the SQL Server conventions
        var modelBuilder = new ModelBuilder(ModelBuildingHelper.GetSqlServerConventionSet());

        return modelBuilder;
    }

    public IModel BuildModel(string? schemaName = default)
    {
        schemaName ??= string.Empty;

        if (ModelCache.TryGetValue(schemaName, out var model))
            return model;

        var modelBuilder = CreateModelBuilder();

        if (!string.IsNullOrEmpty(schemaName))
            modelBuilder.HasDefaultSchema(schemaName);

        ConfigureModel(modelBuilder);
        model = modelBuilder.FinalizeModel();
        ModelCache.TryAdd(schemaName, model);

        return model;
    }

    private string SetTableName(EntityTypeBuilder entity, string tableName)
    {
        var table = tableName.Replace("{CC}", _companyCode).Replace("{CY}", _companyYear);
        entity.ToTable(table);
        return table;
    }

    private void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InvoiceOSModel>(entity =>
        {
            SetTableName(entity, "PL03{CC}00");
        });
        modelBuilder.Entity<SupplierOSModel>(entity =>
        {
            var tableName = SetTableName(entity, "PL01{CC}00");
        });
        /* // Configure Customer entity
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasMany(e => e.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);
        });

        // Configure Order entity
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderDate).IsRequired();
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
        }); */
    }
}