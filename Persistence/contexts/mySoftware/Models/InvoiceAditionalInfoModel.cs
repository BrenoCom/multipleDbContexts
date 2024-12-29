using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Persistence.contexts.mySoftware.Models;

public class InvoiceAditionalInfoModel
{
    [Key]
    public int idInvoiceAditionalInfo { get; set; }
    public int idStatus { get; set; }
    public bool isIF { get; set; }
}