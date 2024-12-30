# Multiple DB Contexts  
This project is a demo showcasing the flexibility of Entity Framework Core, allowing you to define:  
- Dynamic connection strings;  
- Dynamic table names mapped to the same models;  
- The ability to map dynamic columns.  

---

# Demo Setup  
To start the demo:  

1. Run the following command at the project's root to start two SQL Server databases. If you don't have Docker but have SQL Server installed, you can still run the project, but you won't be able to simulate another server with the DB running on a different port:  
```yml
docker compose up -d
```  

2. In the `Persistence/Seeds` folder, you'll find two SQL scripts:  
   - `01 - mySoftwareDB.sql` initializes the main application database and creates the "Company" table (**should only be run once**).  
   - `02 - otherSoftwareDB.sql` creates new databases. Its configurations are defined in the initial `DECLARE` statement. After configuring, run it to create a database with two pre-populated tables (`PL03--00` and `PL01--00`) and a record saved in the "Company" table of the main application DB.  

3. To view the data, you can use the requests in the `WebApi/WebApi.http` file. It contains two calls to the database of the other software and one to the "Company" table.  

---

# How?  
Explanation of the persistence layer:  

1. **Mapping the main application database fields:**  
   First, it's necessary to map the application's database fields. Only then can you retrieve detailed information about the locations of the company's additional data.  
```csharp
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
    // ...

    protected override void OnModelCreating(ModelBuilder modelBuilder){}
}

public class CompanyModel
{
    [Required]
    [Key]
    public int idCompany { get; set; }
    [Required]
    public string name { get; set; } = string.Empty;
    [Required]
    public string serverERP { get; set; } = string.Empty;
    [Required]
    public string baseERP { get; set; } = string.Empty;
    [Required]
    public string codeERP { get; set; } = string.Empty;
    [Required]
    public string yearERP { get; set; } = string.Empty;
    [Required]
    public bool hasIF { get; set; }
    [Required]
    public string serverERPIF { get; set; } = string.Empty;
    [Required]
    public string baseERPIF { get; set; } = string.Empty;
    [Required]
    public string codeERPIF { get; set; } = string.Empty;
    [Required]
    public string yearERPIF { get; set; } = string.Empty;
}
```  

2. **Building the dynamic connection string:**  
   After mapping and confirming that the information is retrieved correctly, construct the connection string dynamically. In this example, I used `IOptions` to fetch the connection string from `appsettings.json`.  

3. **Creating a new DbContext for the other software:**  
   Create the classes [`OtherSoftwareDbContext`](https://github.com/BrenoCom/moltableDbContexts/blob/main/Persistence/contexts/otherSoftware/OtherSoftwareDbContext.cs), [`InvoiceOSModel`](https://github.com/BrenoCom/moltableDbContexts/blob/main/Persistence/contexts/otherSoftware/Models/InvoiceOSModel.CS), and [`SupplierOSModel`](https://github.com/BrenoCom/moltableDbContexts/blob/main/Persistence/contexts/otherSoftware/Models/SupplierOSModel.cs). Ensure you use the correct table names. If you need to add parameters to column names, you'll need to do the same as with table names, which I'll discuss later in the `ModelBuilder`.  

4. **Dynamic table mapping:**  
   [`OtherSoftwareDbContextModelConfiguration`](https://github.com/BrenoCom/moltableDbContexts/blob/main/Persistence/contexts/otherSoftware/OtherSoftwareDbContextModelConfiguration.cs) receives parameters in its constructor. Based on this information, it dynamically maps the tables using `SetTableName`. In this example, only the code and year are modified, but column names can also be dynamically mapped.  

5. **Creating a fully dynamic DbContext:**  
   [`OtherSoftwareDbContextCreator`](https://github.com/BrenoCom/moltableDbContexts/blob/main/Persistence/contexts/otherSoftware/OtherSoftwareDbContextCreator.cs) is instantiated in the service that will use it but only briefly, as its sole purpose is to generate the DbContext. Here, you'll define the server, database, code, or IF using the configuration options from `appsettings.json`.  

---
# Credits

This was a simplified explanation of what happens before using the dynamic DbContext. However, this example still requires additional testing. Suggestions for improvement are welcome.  

Special thanks to `Svyatoslav Danyliv` for providing examples of Multi-tenancy and `Alexander Petrov` for sharing resources on the topic.  
[StackOverflow question here](https://stackoverflow.com/questions/79312019/how-to-dynamically-access-tables-across-multiple-databases-using-entity-framewor?noredirect=1#comment139862945_79312019)
