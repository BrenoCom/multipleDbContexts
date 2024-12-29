using System.ComponentModel.DataAnnotations;

namespace Persistence.contexts.mySoftware.Models;

public class SupplierAditionalInfoModel
{
    [Key]
    public int idSupplierAditionalInfoInfo { get; set; }
    public int idStatus { get; set; }
    public string TypeSupplier { get; set; } = string.Empty;
}