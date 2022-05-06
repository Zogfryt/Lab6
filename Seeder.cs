using Code_First.Data;
using Code_First.Data.Models;

namespace Code_First;

struct Pair
{
    public int First;
    public int Second;
}
public static class Seeder
{
    const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    
    private static readonly string[] Names = {"James", "Bartłomiej", "Grzegorz", "Marcin"};
    private static readonly string[] Surnames = {"Wajdzik", "Cameron", "Kosakowski", "Scott"};
    private static readonly string[] Cities = {"Wadowice", "Kraków", "New York", "Szczecin"};
    private static readonly string[] Prefixes = {"al", "ul", "os"};

    private static readonly string[] DNamePref = {"Ciężki", "Lekki", "Mokry", "Mocna", "Gorące"};
    private static readonly string[] DNameSuff = {"Kaszel", "Katar", "Zatoki", "Plecy", "Oczy"};

    private static readonly string[] MNamePref =
        {"Mocny", "Piekielny", "Skuteczny", "Ziołowy", "Kapucyński", "Tybetański"};

    private static readonly string[] MNameSuff = {"Syrop", "Maść", "TAbletki", "Antybiotyk", "Proszek"};
    
    public static void Seed(int nPatients=0, int nDiagnoses=0, int nVisitations=0, int nMedications=0, int nPatientMedications=0)
    {
        var ctx = new HospitalContext();

        using (ctx)
        {
            int maxP;
            try
            {
                maxP = ctx.Patients.Select(p => p.PatientId).Max();
            }
            catch (InvalidOperationException)
            {
                maxP = 0;
            }
            int maxM;
            try
            {
                maxM = ctx.Medicamentses.Select(p => p.MedicamentId).Max();
            }
            catch (InvalidOperationException)
            {
                maxM = 0;
            }
            var patients = GeneratePatients(nPatients, maxP);
            var meds = GenerateMedicaments(nMedications, maxM);
            
            ctx.Patients.AddRange(patients);
            ctx.Medicamentses.AddRange(meds);
            ctx.SaveChanges();

            var patientIndexes = ctx.Patients.Select(x => x.PatientId).ToArray();
            var medsIndexes = ctx.Medicamentses.Select(x => x.MedicamentId).ToArray();
            int dMax;
            try
            {
                dMax = ctx.Diagnoses.Select(p => p.DiagnoseId).Max();
            }
            catch (InvalidOperationException)
            {
                dMax = 0;
            }
            var diagnoses = GenerateDiagnoses(patientIndexes,nDiagnoses, dMax);

            ctx.Diagnoses.AddRange(diagnoses);

            int vMax;
            try
            {
                vMax = ctx.Visitations.Select(p => p.VisitationId).Max();
            }
            catch (InvalidOperationException )
            {
                vMax = 0;
            }
            var visits = GenerateVisitations(patientIndexes, nVisitations, vMax);
            ctx.Visitations.AddRange(visits);

            var pairs = ctx.PatientMedicamentes.Select(x => new Pair {First = x.PatientId, Second = x.MedicamentId}).ToList();
            var patientmeds = GeneratePatientMedicaments(patientIndexes, medsIndexes, nPatientMedications, pairs);
            ctx.PatientMedicamentes.AddRange(patientmeds);

            ctx.SaveChanges();
        }
        

    }

    private static List<Patient> GeneratePatients(int number,int max)
    {
        Random rnd = new Random();
        List<Patient> patients = new List<Patient>();
        for(var i=max+1; i<=max+number; i++)
        {
            string name = Names[rnd.Next(Names.Length)];
            string surname = Surnames[rnd.Next(Surnames.Length)];
            string email = $"{name.Substring(0, 5)}{surname}@gmail.com";
            string address =
                $"{Prefixes[rnd.Next(Prefixes.Length)]}. {Cities[rnd.Next(Cities.Length)]} {rnd.Next(100)}";
            bool insurance = rnd.NextDouble() > 0.5;
            patients.Add(new Patient{FirstName = name, LastName = surname, Email = email, Address = address, HasInsure = insurance, PatientId = i});
        }
        return patients;
    }

    private static List<Diagnose> GenerateDiagnoses(int[] indexes, int number, int max)
    {
        Random rand = new Random();
        List<Diagnose> diagnoses = new List<Diagnose>();
        for (var i = max+1; i <= max+number; i++)
        {
            int p_ID = indexes[rand.Next(indexes.Length)];
            int length = rand.Next(150) + 100;
            string description = new string(Enumerable.Repeat(Chars, length)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
            string name = $"{DNamePref[rand.Next(DNamePref.Length)]} {DNameSuff[rand.Next(DNameSuff.Length)]}";
            diagnoses.Add(new Diagnose{Comments = description, Name = name, PatientId = p_ID, DiagnoseId = i});
            
                
        }

        return diagnoses;
    }

    private static List<Visitation> GenerateVisitations(int[] indexes, int number, int max)
    {
        DateTime starter = new DateTime(1995, 1, 1);
        int range = (DateTime.Today - starter).Days;
        Random rand = new Random();
        List<Visitation> visits = new List<Visitation>();
        for (int i = max+1; i<=max+number; i++)
        {
            int p_ID = indexes[rand.Next(indexes.Length)];
            int length = rand.Next(150) + 100;
            string description = new string(Enumerable.Repeat(Chars, length)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
            DateTime start = new DateTime(1995, 1, 1);
            DateOnly date = DateOnly.FromDateTime(start.AddDays(rand.Next(range)));

            visits.Add(new Visitation {Comments = description, Date = date, PatientId = p_ID, VisitationId = i});
        }

        return visits;
    }

    private static List<Medicaments> GenerateMedicaments(int number, int max)
    {
        Random rand = new Random();
        var meds = new List<Medicaments>();
        for (int i = max+1; i<=max+number; i++)
        {
            string name = $"{MNamePref[rand.Next(MNamePref.Length)]} {MNameSuff[rand.Next(MNameSuff.Length)]} ";
            meds.Add(new Medicaments {Name = name, MedicamentId = i});
        }
        return meds;
    }

    private static List<PatientMedicament> GeneratePatientMedicaments(int[] pIndexes, int[] mIndexes, int number,List<Pair> pairs)
    {
        Random rand = new Random();
        List<PatientMedicament> meds = new List<PatientMedicament>();
        List<Pair> everyPair = new List<Pair>();
        foreach (var i in pIndexes)
        {
            foreach (var j in mIndexes)
            {
                everyPair.Add(new Pair{First = i, Second = j});
            }
        }

        foreach (var par in pairs)
        {
            if (everyPair.Contains(par))
            {
                everyPair.Remove(par);
            }
        }
        for (int i = 0; i < number; i++)
        {
            var par = everyPair[rand.Next(everyPair.Count)];
            everyPair.Remove(par);
            meds.Add(new PatientMedicament{MedicamentId = par.Second, PatientId = par.First});
        }

        return meds;
    }
}