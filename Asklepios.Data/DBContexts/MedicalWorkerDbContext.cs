using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Asklepios.Core.Enums;

namespace Asklepios.Data
{
    public class MedicalWorkerDbContext : DbContext, IMedicalWorkerModuleRepository
    {
        public List<Visit> GetFutureVisitsByMedicalWorkerId(long id)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetHistoricalVisitsByMedicalWorkerId(long id)
        {
            throw new NotImplementedException();
        }

        public List<Location> GetLocations()
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerByPersonId(long personId)
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerData()
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientById(int id)
        {
            throw new NotImplementedException();
        }

        public List<VisitReview> GetReviewsByMedicalWorkerId(long id)
        {
            throw new NotImplementedException();
        }

        public Visit GetAvailableVisitById(long currentVisitId)
        {
            throw new NotImplementedException();
        }

        public Visit GetHistoricalVisitById(long currentVisitId)
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerById(int id)
        {
            throw new NotImplementedException();
        }

        public Visit GetBookedVisitById(long currentVisitId)
        {
            throw new NotImplementedException();
        }

        public MedicalService GetMedicalServiceById(long serviceToAdd)
        {
            throw new NotImplementedException();
        }

        public void AddRecommendation(Recommendation recommendationToAdd)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecommendation(long id)
        {
            throw new NotImplementedException();
        }

        public List<MedicalService> GetMedicalServices()
        {
            throw new NotImplementedException();
        }

        public void AddMedicalReferral(MedicalReferral medicalReferral)
        {
            throw new NotImplementedException();
        }

        public MedicalReferral GetMedicalReferralById(long medicalReferralIdToRemove)
        {
            throw new NotImplementedException();
        }

        public void RemoveMedicalReferralById(long medicalReferralIdToRemove)
        {
            throw new NotImplementedException();
        }

        public void AddMedicine(IssuedMedicine issuedMedicineToAdd)
        {
            throw new NotImplementedException();
        }

        public void AddPrescription(Prescription prescription)
        {
            throw new NotImplementedException();
        }

        public void RemoveIssuedMedicineById(long medicineIdToRemove)
        {
            throw new NotImplementedException();
        }

        public void UpdatePrescription(Prescription prescription)
        {
            throw new NotImplementedException();
        }

        public List<Prescription> GetPrescriptions()
        {
            throw new NotImplementedException();
        }

        public Prescription GetPrescriptionById(long prescriptionIdToRemove)
        {
            throw new NotImplementedException();
        }

        public void RemovePrescriptionById(long prescriptionIdToRemove)
        {
            throw new NotImplementedException();
        }

        public void AddMedicalTestResult(MedicalTestResult medicalTestResult)
        {
            throw new NotImplementedException();
        }

        public void UpdateTestResultFile(IFormFile medicalTestFile, Visit visit, Visit hostEnvironmentPath)
        {
            throw new NotImplementedException();
        }

        public void UpdateTestResultFile(IFormFile medicalTestFile, Visit visit, string hostEnvironmentPath)
        {
            throw new NotImplementedException();
        }

        public void AddMedicalTestResult(MedicalTestResult medicalTestResult, IFormFile formFile, string hostPath)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetPastVisitsByMedicalWorkerId(long id)
        {
            throw new NotImplementedException();
        }

        public byte[] GetDocument(string documentPath, string webRootPath)
        {
            throw new NotImplementedException();
        }

        public void RemoveTestResult(long id, long id1, string webRootPath)
        {
            throw new NotImplementedException();
        }

        public Person GetPersonById(long personId)
        {
            throw new NotImplementedException();
        }

        public void AddNotification(long id1, NotificationType testResult, long id2, DateTimeOffset now, long visitId)
        {
            throw new NotImplementedException();
        }
    }
}
