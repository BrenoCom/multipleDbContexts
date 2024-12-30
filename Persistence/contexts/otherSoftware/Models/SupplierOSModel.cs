using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Persistence.contexts.otherSoftware.Models;


[PrimaryKey(nameof(SupplierCode))]
public class SupplierOSModel
{

    /// <value>Property <c>SupplierCode</c> represents the column PL01001 with the note Supplier Code</value>
    [Required]
    [Column(name: "PL01001", TypeName = "VARCHAR(10)")]
    [MaxLength(10)]
    public string SupplierCode { get; set; } = string.Empty;
    /// <value>Property <c>SupplierName</c> represents the column PL01002 with the note Supplier Name</value>
    [Required]
    [Column(name: "PL01002", TypeName = "VARCHAR(35)")]
    [MaxLength(35)]
    public string SupplierName { get; set; } = string.Empty;
    /// <value>Property <c>AddressLine1</c> represents the column PL01003 with the note Address Line 1</value>
    [Required]
    [Column(name: "PL01003", TypeName = "VARCHAR(35)")]
    [MaxLength(35)]
    public string AddressLine1 { get; set; } = string.Empty;
    /// <value>Property <c>AddressLine2</c> represents the column PL01004 with the note Address Line 2</value>
    [Required]
    [Column(name: "PL01004", TypeName = "VARCHAR(35)")]
    [MaxLength(35)]
    public string AddressLine2 { get; set; } = string.Empty;
    /// <value>Property <c>AddressLine3</c> represents the column PL01005 with the note Address Line 3</value>
    [Required]
    [Column(name: "PL01005", TypeName = "VARCHAR(35)")]
    [MaxLength(35)]
    public string AddressLine3 { get; set; } = string.Empty;


}