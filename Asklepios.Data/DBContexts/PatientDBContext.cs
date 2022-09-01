using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Data
{
    public class PatientDBContext : DbContext
    {

        public PatientDBContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public Patient CurrentPatient { get; set; }
        public DbSet<Visit> AvailableVisits { get; set; }

    }
}
