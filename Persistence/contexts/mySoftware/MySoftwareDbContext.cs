using Microsoft.EntityFrameworkCore;
using Persistence.contexts.mySoftware.Models;

namespace Persistence.contexts.mySoftware;

public partial class MySoftwareDbContext : DbContext
{
    public MySoftwareDbContext()
    {
    }

    public MySoftwareDbContext(DbContextOptions<MySoftwareDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<CompanyModel> Company { get; set; }
    public virtual DbSet<StatusModel> Status { get; set; }
    public virtual DbSet<SupplierAditionalInfoModel> SupplierAditionalInfo { get; set; }
    public virtual DbSet<InvoiceAditionalInfoModel> InvoiceAditionalInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}