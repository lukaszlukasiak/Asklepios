using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Asklepios.Data
{
    public class MedicalWorkerDbContext : DbContext, IMedicalWorkerModuleRepository
    {
        public MedicalWorker GetMedicalWorkerByUserId(long personId)
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerData()
        {
            throw new NotImplementedException();
        }

        public Visit GetVisitById(long currentVisitId)
        {
            throw new NotImplementedException();
        }
    }
}
