using System.ComponentModel.DataAnnotations;

namespace Persistence.contexts.mySoftware.Models;

public class StatusModel
{
    [Key]
    public int idStatus { get; set; }
    public string description { get; set; } = string.Empty;
}