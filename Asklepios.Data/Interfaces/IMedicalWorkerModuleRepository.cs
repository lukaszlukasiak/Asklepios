using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.Interfaces
{
    public interface IMedicalWorkerModuleRepository
    {
        MedicalWorker GetMedicalWorkerData();
        MedicalWorker GetMedicalWorkerByUserId(long personId);
        Visit GetVisitById(long currentVisitId);
    }
}
