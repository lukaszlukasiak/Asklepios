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
        public List<Visit> GetFutureVisitsByMedicalWorkerId(long id)
        {
            List<Visit> visits = PatientMockDB.GetFutureVisits().Where(c => c.MedicalWorker.Id == id).ToList();
            return visits;
        }

        public List<Visit> GetHistoricalVisitsByMedicalWorkerId(long id)
        {
            List<Visit> visits = PatientMockDB.HistoricalVisits.Where(c => c.MedicalWorker.Id == id).ToList();
            return visits;
        }

        public List<Location> GetLocations()
        {
            return PatientMockDB.Locations;
        }

        public MedicalWorker GetMedicalWorkerByUserId(long personId)
        {
            List<MedicalWorker> medicalWorkers = PatientMockDB.GetMedicalWorkers().ToList();
            MedicalWorker medicalWorker = medicalWorkers.Where(c => c.Person.Id == personId).FirstOrDefault();
            return medicalWorker;
        }

        public MedicalWorker GetMedicalWorkerData()
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientById(int id)
        {
            Patient patient = PatientMockDB.GetPatientById(id);
            return patient;
        }

        public List<VisitReview> GetReviewsByMedicalWorkerId(long id)
        {
            List<Visit> visits = GetHistoricalVisitsByMedicalWorkerId(id);
            if (visits != null)
            {
                return visits.Where(c => c.VisitReview != null).Select(c => c.VisitReview).ToList();
            }
            else
            {
                return null;
            }
        }

        public Visit GetAvailableVisitById(long currentVisitId)
        {
            Visit visit = PatientMockDB.GetAvailableVisitById(currentVisitId);
            return visit;
        }
        public Visit GetHistoricalVisitById(long currentVisitId)
        {
            Visit visit = PatientMockDB.GetHistoricalVisitById(currentVisitId);
            return visit;
        }

        public MedicalWorker GetMedicalWorkerById(int id)
        {
            List<MedicalWorker> medicalWorkers = PatientMockDB.GetMedicalWorkers().ToList();
            MedicalWorker medicalWorker = medicalWorkers.Where(c => c.Id == id).FirstOrDefault();
            return medicalWorker;
        }
    }
}
