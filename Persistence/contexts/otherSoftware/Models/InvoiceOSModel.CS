using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Persistence.contexts.otherSoftware.Models;

[Keyless]
public class InvoiceOSModel
{

    /// <value>Property <c>SupplierCode</c> represents the column PL03001 with the note Supplier Code</value>
    [Required]
    [Column(name: "PL03001", TypeName = "VARCHAR(10)")]
    [MaxLength(10)]
    public string SupplierCode { get; set; } = string.Empty;
    /// <value>Property <c>InvoiceNo</c> represents the column PL03002 with the note Invoice Number</value>
    [Required]
    [Column(name: "PL03002", TypeName = "VARCHAR(25)")]
    [MaxLength(25)]
    public string InvoiceNo { get; set; } = string.Empty;
    /// <value>Property <c>TransactioNo</c> represents the column PL03003 with the note Transaction Number General ledger transaction number for the transaction created when the invoice was created. If the invoice is first entered as an unauthorized invoice, this field will hold the transaction number for the transaction created for the authorized invoice and column PL03018 will hold the transaction number for the authorized invoice transaction.</value>
    [Required]
    [Column(name: "PL03003", TypeName = "VARCHAR(9)")]
    [MaxLength(9)]
    public string TransactioNo { get; set; } = string.Empty;
    /// <value>Property <c>InvoiceDate</c> represents the column PL03004 with the note Invoice Date</value>
    [Required]
    [Column(name: "PL03004", TypeName = "DATE")]
    public DateTime InvoiceDate { get; set; }
    /// <value>Property <c>BookEntrDate</c> represents the column PL03005 with the note Book-Entry Date Book-entry for the transaction created when the invoice was entered. If the invoice is first entered as an unauthorized invoice, this field will hold the book-entry date for the transaction created for the authorized invoice and column PL03019 will hold the book-entry date for the authorized invoice transaction.</value>
    [Required]
    [Column(name: "PL03005", TypeName = "DATE")]
    public DateTime BookEntrDate { get; set; }
    /// <value>Property <c>DueDate</c> represents the column PL03006 with the note Due Date</value>
    [Required]
    [Column(name: "PL03006", TypeName = "DATE")]
    public DateTime DueDate { get; set; }

}