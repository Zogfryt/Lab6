using System.ComponentModel.DataAnnotations;

namespace Code_First.Data.Models;

public class Medicaments
{
    [Key]
    public int MedicamentId { get; set; }
    
    [MaxLength(50)]
    public string Name { get; set; }
    
    public ICollection<PatientMedicament> PatientMedicaments { get; set; }
}