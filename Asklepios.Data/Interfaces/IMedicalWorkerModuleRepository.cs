using Asklepios.Core.Enums;
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
        void AddMedicalReferral(MedicalReferral medicalReferral);

        void AddMedicalTestResult(MedicalTestResult medicalTestResult, IFormFile formFile, string hostPath);

        void AddMedicine(IssuedMedicine issuedMedicineToAdd);

        void AddNotification(long id1, NotificationType testResult, long id2, DateTimeOffset now, long visitId);

        void AddPrescription(Prescription prescription);

        void AddRecommendation(Recommendation recommendationToAdd);
        void DeleteRecommendation(long id);
        IQueryable<Visit> GetVisitsByMedicalWorkerId(long id);

        Visit GetFutureVisitById(long currentVisitId);

        Visit GetBookedVisitById(long currentVisitId);

        byte[] GetDocument(string documentPath, string webRootPath);

        IQueryable<Visit> GetFutureVisitsByMedicalWorkerId(long id);

        Visit GetHistoricalVisitById(long currentVisitId);

        IQueryable<Visit> GetHistoricalVisitsByMedicalWorkerId(long id);

        Location GetLocationById(long locationId);

        List<Location> GetLocations();

        MedicalPackage GetMedicalPackageById(long medicalPackageId);

        MedicalReferral GetMedicalReferralById(long medicalReferralIdToRemove);

        MedicalRoom GetMedicalRoomById(long medicalRoomId);

        MedicalService GetMedicalServiceById(long serviceToAdd);

        List<MedicalService> GetMedicalServices();

        MedicalWorker GetMedicalWorkerById(long id);

        MedicalWorker GetMedicalWorkerByPersonId(long personId);

        NFZUnit GetNFZUnitById(long nFZUnitId);

        Patient GetPatientById(long id);

        Person GetPersonById(long personId);

        Prescription GetPrescriptionById(long prescriptionIdToRemove);

        List<Prescription> GetPrescriptions();

        List<VisitReview> GetReviewsByMedicalWorkerId(long id);

        User GetUserById(long userId);

        Visit GetVisitById(long id);

        VisitCategory GetVisitCategoryById(long visitCategoryId);

        void RemoveIssuedMedicineById(long medicineIdToRemove);

        void RemoveMedicalReferralById(long medicalReferralIdToRemove);
        void RemovePrescriptionById(long prescriptionIdToRemove);

        void RemoveTestResult(long id, long id1, string webRootPath);

        void UpdatePrescription(Prescription prescription);
        void UpdateTestResultFile(IFormFile medicalTestFile, Visit visit, string webRootPath);
        void UpdateVisit(Visit visit);
        Visit GetBookedVisitByIdANT(long currentVisitId);
        MedicalWorker GetMedicalWorkerByUserId(long id);
        MedicalTestResult GetMedicalTestResultById(long idL);
        List<MedicalReferral> GetMedicalReferralsByPatientId(long id);
        List<MedicalTestResult> GetMedicalTestResultsByPatientId(long id);
        List<Prescription> GetPrescriptionsByPatientId(long id);
        List<Visit> GetHistoricalVisitsByPatientId(long id);
        string SaveFile(IFormFile formFile, StorageFolderType type, string basePath);
        void RemoveFile(string v, string webRootPath);
    }
}
