using Code_First.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_First.Data;

public class HospitalContext : DbContext
{
    public DbSet<Diagnose> Diagnoses { get; set; }
    public DbSet<Medicaments> Medicamentses { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Visitation> Visitations { get; set; }
    public DbSet<PatientMedicament> PatientMedicamentes { get; set; }
    public DbSet<Doctor> Doctores { get; set; }
    public void ApplicationDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=HospitalDatabase;Username=postgres;Password=xxxxxxxx");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PatientMedicament>().HasKey(ep => new {ep.PatientId, ep.MedicamentId});
        modelBuilder.Entity<Diagnose>().Property(x => x.DiagnoseDate).HasDefaultValue(new DateOnly(2000, 1, 1));
        
        //Doctor Problems solution
        //modelBuilder.Entity<Visitation>().Property(x => x.DoctorId).HasDefaultValue(null);
    }
}