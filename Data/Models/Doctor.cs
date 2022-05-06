using System.ComponentModel.DataAnnotations;

namespace Code_First.Data.Models;

public class Doctor
{
    [Key]
    public int DoctorId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Surname { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Specialization { get; set; }
    
    public ICollection<Visitation> Visitations { get; set; }
}