using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Data
{
    public class PatientsDBContext : DbContext
    {
        public Patient CurrentPatient { get; set; }
        public DbSet<Visit> AvailableVisits { get; set; }

    }
}
