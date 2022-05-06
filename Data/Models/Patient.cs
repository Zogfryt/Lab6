using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Dataflow;

namespace Code_First.Data.Models;

public class Patient
{
    [Key]
    public int PatientId { get; set; }
    
    [MaxLength(50)]
    [Required]
    public string FirstName { get; set; }
    
    [MaxLength(50)]
    [Required]
    public string LastName { get; set; }

    [MaxLength(250)]
    [Required]
    public string Address { get; set;}
    
    [MaxLength(80)]
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    
    [Required]
    public bool HasInsure { get; set; }
    
    public ICollection<Diagnose> Diagnoses {get; set; }
    public ICollection<Visitation> Visitations { get; set; }
    public ICollection<PatientMedicament> PatientMedicaments { get; set; }

}