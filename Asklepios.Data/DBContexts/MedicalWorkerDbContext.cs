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
    public class MedicalWorkerDbContext : DbContext, IMedicalWorkerModuleRepository
    {
        public MedicalWorker GetMedicalWorkerData()
        {
            throw new NotImplementedException();
        }
    }
}
