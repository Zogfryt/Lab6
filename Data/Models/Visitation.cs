using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_First.Data.Models;

public class Visitation
{
    [Key]
    public int VisitationId { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [MaxLength(250)]
    public string Comments { get; set; }
    
    [Required]
    public int PatientId { get; set;}
    
    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }
    public int DoctorId { get; set; }
    
    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set;}

}