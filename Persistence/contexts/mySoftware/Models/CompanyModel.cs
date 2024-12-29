using System.ComponentModel.DataAnnotations;

namespace Persistence.contexts.mySoftware.Models;

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