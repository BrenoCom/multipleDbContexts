using Microsoft.EntityFrameworkCore;
using Persistence.contexts.otherSoftware.Models;

namespace Persistence.contexts.otherSoftware;

public class OtherSoftwareDbContext : DbContext
{
    public DbSet<InvoiceOSModel> Incoices { get; set; }
    public DbSet<SupplierOSModel> Suppliers { get; set; }



    public OtherSoftwareDbContext(DbContextOptions<OtherSoftwareDbContext> options) : base(options)
    {
    }
}