using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data
{
    public class CustomerServiceDbContext : DbContext, ICustomerServiceModuleRepository
    {
        public IEnumerable<Visit> GetAvailableVisits()
        {
            throw new NotImplementedException();
        }

        public Patient GetCurrentPatientData()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetLocations()
        {
            throw new NotImplementedException();
        }
    }
}
