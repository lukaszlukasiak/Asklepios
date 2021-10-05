using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Asklepios.Data
{
    public class AdministrationDbContext : DbContext, IAdministrationModuleRepository
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
