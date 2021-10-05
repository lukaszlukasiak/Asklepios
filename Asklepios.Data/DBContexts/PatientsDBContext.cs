using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data
{
    public class PatientsDBContext : DbContext
    {
        public Patient CurrentPatient { get; set; }
        public DbSet<Visit> AvailableVisits { get; set; }

    }
}
