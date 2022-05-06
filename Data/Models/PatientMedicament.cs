using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_First.Data.Models;

public class PatientMedicament
{
    [Key, Column(Order = 0)]
    public int PatientId {get; set; }
    
    [Key, Column(Order = 1)]
    public int MedicamentId {get; set; }
    
    [ForeignKey("PatientId")]
    public Patient Patient {get; set; }
    
    [ForeignKey("MedicamentId")]
    public Medicaments Medicament { get; set;}
}