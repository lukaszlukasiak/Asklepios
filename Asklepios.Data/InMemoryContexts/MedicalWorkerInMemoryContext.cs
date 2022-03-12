using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.InMemoryContexts
{
    public class MedicalWorkerInMemoryContext : IMedicalWorkerModuleRepository
    {
        public MedicalWorker GetMedicalWorkerByUserId(long personId)
        {
            List<MedicalWorker> medicalWorkers = PatientMockDB.GetMedicalWorkers().ToList();
            MedicalWorker medicalWorker = medicalWorkers.Where(c=>c.Person.Id==personId).FirstOrDefault();
            return medicalWorker;
        }

        public MedicalWorker GetMedicalWorkerData()
        {
            throw new NotImplementedException();
        }

        public Visit GetVisitById(long currentVisitId)
        {
            Visit visit = PatientMockDB.GetAvailableVisitById(currentVisitId);
            return visit;
        }
    }
}
