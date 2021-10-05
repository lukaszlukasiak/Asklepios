using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.Interfaces
{
    public interface ICustomerServiceModuleRepository
    {
        Patient GetCurrentPatientData();
        IEnumerable<Visit> GetAvailableVisits();
        IEnumerable<Location> GetLocations();

    }
}
