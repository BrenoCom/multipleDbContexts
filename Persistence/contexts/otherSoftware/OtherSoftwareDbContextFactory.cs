using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence.contexts.otherSoftware;

public class OtherSoftwareDbContextFactory : IDesignTimeDbContextFactory<OtherSoftwareDbContext>
{
    /// <summary>
    /// Creates a new instance of AppDbContext for design-time tools like migrations.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    /// <returns>An instance of AppDbContext.</returns>
    public OtherSoftwareDbContext CreateDbContext(string[] args)
    {
        // Define the connection string
        var connectionString = "Server=localhost,1419;Database=SOMultitenancy;User Id=sa;Password=Password12!;Encrypt=true;TrustServerCertificate=true";

        // Configure DbContextOptions
        var optionsBuilder = new DbContextOptionsBuilder<OtherSoftwareDbContext>();
        //optionsBuilder
        //    .UseSqlServerMultiTenant(connectionString, null, IscalaDbContextModelConfiguration.BuildModel);

        return new OtherSoftwareDbContext(optionsBuilder.Options);
    }
}