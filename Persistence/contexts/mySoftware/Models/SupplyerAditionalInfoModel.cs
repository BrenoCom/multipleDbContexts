using System.ComponentModel.DataAnnotations;

namespace Persistence.contexts.mySoftware.Models;

public class SupplyerAditionalInfoModel
{
    [Key]
    public int idSupplyerAditionalInfoInfo { get; set; }
    public int idStatus { get; set; }
    public string TypeSupplyer { get; set; } = string.Empty;
}