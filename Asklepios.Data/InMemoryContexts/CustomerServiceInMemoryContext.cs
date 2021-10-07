using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.InMemoryContexts
{
    public class CustomerServiceInMemoryContext : ICustomerServiceModuleRepository
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
