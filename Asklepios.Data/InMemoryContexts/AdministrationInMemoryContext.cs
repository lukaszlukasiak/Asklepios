using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.InMemoryContexts
{
    public class AdministrationInMemoryContext : IAdministrationModuleRepository
    {
        public Patient GetCurrentPatient()
        {
            throw new NotImplementedException();
        }

        public MedicalPackage GetMedicalPackage()
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorker()
        {
            throw new NotImplementedException();
        }
    }
}
