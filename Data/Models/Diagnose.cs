using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_First.Data.Models;

public class Diagnose
{
    [Key]
    public int DiagnoseId { get; set;}
    
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }

    [MaxLength(250)]
    public string Comments { get; set; }
    
    public int PatientId { get; set;}
    
    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }
    
    public DateOnly DiagnoseDate { get; set; }
    
}