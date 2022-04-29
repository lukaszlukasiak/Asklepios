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
        Visit GetAvailableVisitById(long currentVisitId);
        List<Visit> GetFutureVisitsByMedicalWorkerId(long id);
        List<Visit> GetHistoricalVisitsByMedicalWorkerId(long id);
        List<Location> GetLocations();
        List<VisitReview> GetReviewsByMedicalWorkerId(long id);
        Patient GetPatientById(int id);
        Visit GetHistoricalVisitById(long currentVisitId);
        MedicalWorker GetMedicalWorkerById(int id);
    }
}
