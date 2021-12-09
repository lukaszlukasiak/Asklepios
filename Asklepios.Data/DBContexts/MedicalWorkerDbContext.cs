using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Asklepios.Data
{
    public class MedicalWorkerDbContext : DbContext, IMedicalWorkerModuleRepository
    {
        public MedicalWorker GetMedicalWorkerData()
        {
            throw new NotImplementedException();
        }
    }
}
