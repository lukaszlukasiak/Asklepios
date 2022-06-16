using Asklepios.Core.Models;
using Microsoft.AspNetCore.Http;
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
        Visit GetBookedVisitById(long currentVisitId);
        MedicalService GetMedicalServiceById(long serviceToAdd);
        void AddRecommendation(Recommendation recommendationToAdd);
        void DeleteRecommendation(long id);
        List<MedicalService> GetMedicalServices();
        void AddMedicalReferral(MedicalReferral medicalReferral);
        MedicalReferral GetMedicalReferralById(long medicalReferralIdToRemove);
        void RemoveMedicalReferralById(long medicalReferralIdToRemove);
        void AddPrescription(Prescription prescription);
        void AddMedicine(IssuedMedicine issuedMedicineToAdd);
        void RemoveIssuedMedicineById(long medicineIdToRemove);
        void UpdatePrescription(Prescription prescription);
        List<Prescription> GetPrescriptions();
        Prescription GetPrescriptionById(long prescriptionIdToRemove);
        void RemovePrescriptionById(long prescriptionIdToRemove);
        void AddMedicalTestResult(MedicalTestResult medicalTestResult, IFormFile formFile,string hostPath);
        void UpdateTestResultFile(IFormFile medicalTestFile, Visit visit, string webRootPath);
        byte[] GetDocument(string documentPath, string webRootPath);
        void RemoveTestResult(long id, long id1, string webRootPath);
    }
}
