using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Asklepios.Data.InMemoryContexts
{
    public class MedicalWorkerInMemoryContext : IMedicalWorkerModuleRepository
    {
        public List<Visit> GetFutureVisitsByMedicalWorkerId(long id)
        {
            List<Visit> visits = PatientMockDB.FutureVisits.Where(c => c.MedicalWorker.Id == id).ToList();
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

        public MedicalWorker GetMedicalWorkerByPersonId(long personId)
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

        public Visit GetBookedVisitById(long currentVisitId)
        {
            Visit visit = PatientMockDB.GetBookedVisitById(currentVisitId);
            return visit;
        }

        public MedicalService GetMedicalServiceById(long serviceToAdd)
        {
            MedicalService service = PatientMockDB.GetMedicalServiceById(serviceToAdd);
            return service;

        }

        public void AddRecommendation(Recommendation recommendationToAdd)
        {
            recommendationToAdd.Id = PatientMockDB.Recommendations.Max(c => c.Id)+1;
            PatientMockDB.Recommendations.Add(recommendationToAdd);
        }

        public void DeleteRecommendation(long id)
        {
            Recommendation recommendation = PatientMockDB.Recommendations.Where(c => c.Id == id).FirstOrDefault();
            if (recommendation!=null)
            {
                PatientMockDB.Recommendations.Remove(recommendation);
            }
            
        }

        public List<MedicalService> GetMedicalServices()
        {
            return PatientMockDB.MedicalServices;
        }

        public void AddMedicalReferral(MedicalReferral medicalReferral)
        {
            long id = PatientMockDB.MedicalReferrals.Max(c => c.Id) + 1;
            medicalReferral.Id = id;
            PatientMockDB.MedicalReferrals.Add(medicalReferral);
        }

        public MedicalReferral GetMedicalReferralById(long medicalReferralIdToRemove)
        {
            MedicalReferral medicalReferral = PatientMockDB.MedicalReferrals.Find(c => c.Id == medicalReferralIdToRemove);
            return medicalReferral;
        }

        public void RemoveMedicalReferralById(long medicalReferralIdToRemove)
        {
            MedicalReferral medicalReferral = PatientMockDB.MedicalReferrals.Find(c => c.Id == medicalReferralIdToRemove);
            PatientMockDB.MedicalReferrals.Remove(medicalReferral);
        }

        public void AddPrescription(Prescription prescription)
        {
            long newId = PatientMockDB.Prescriptions.Max(c => c.Id) + 1;
            prescription.Id = newId;
            PatientMockDB.Prescriptions.Add(prescription);
        }

        public void AddMedicine(IssuedMedicine issuedMedicineToAdd)
        {
            long newId = PatientMockDB.IssuedMedicines.Max(c => c.Id) + 1;
            issuedMedicineToAdd.Id = newId;
            PatientMockDB.IssuedMedicines.Add(issuedMedicineToAdd);
        }

        public void RemoveIssuedMedicineById(long medicineIdToRemove)
        {
            int index = PatientMockDB.IssuedMedicines.FindIndex(c => c.Id == medicineIdToRemove);
            if (index>=0)
            {
                PatientMockDB.IssuedMedicines.RemoveAt(index);
            }
        }

        public void UpdatePrescription(Prescription prescription)
        {

        }

        public List<Prescription> GetPrescriptions()
        {
            return PatientMockDB.Prescriptions;
        }

        public Prescription GetPrescriptionById(long prescriptionId)
        {
            return PatientMockDB.Prescriptions.FirstOrDefault(c => c.Id == prescriptionId);
        }

        public void RemovePrescriptionById(long prescriptionIdToRemove)
        {
            int index= PatientMockDB.Prescriptions.FindIndex(c => c.Id == prescriptionIdToRemove);
            if (index>=0)
            {
                Prescription prescription = GetPrescriptionById(prescriptionIdToRemove);
                foreach (IssuedMedicine item in prescription.IssuedMedicines)
                {
                    RemoveIssuedMedicineById(item.Id);
                }
                PatientMockDB.Prescriptions.RemoveAt(index);
            }
        }

        public void AddMedicalTestResult(MedicalTestResult medicalTestResult,IFormFile file, string hostEnvironmentPath)
        {
            long newId = PatientMockDB.MedicalTestResults.Max(c => c.Id) + 1;
            medicalTestResult.Id = newId;
            string filePath = SaveMedicalTestFile(file,  hostEnvironmentPath,medicalTestResult.Patient.Person.FullName);
            medicalTestResult.DocumentPath = filePath;
            PatientMockDB.MedicalTestResults.Add(medicalTestResult);
        }
        //public void UpdatePersonImage(IFormFile imageFile, Person person, string hostEnvironmentPath)
        //{
        //    string imagePath = SaveImage(person.ImageFile,  hostEnvironmentPath);
        //    person.ImageFilePath = imagePath;
        //}
        private string SaveMedicalTestFile(IFormFile formFile, string basePath, string patientName)
        {
            string path = null;
            path = Path.Combine("MedicalTestResults" ); 
            string extension = Path.GetExtension(formFile.FileName);
            string myUniqueFileName = null;
            string fullFileName = null;

            do
            {
                myUniqueFileName = string.Format(@"{0}{1}", Guid.NewGuid(), extension);
                fullFileName = Path.Combine(basePath, path, myUniqueFileName);
            } while (System.IO.File.Exists(fullFileName));

            using (var fileStream = new FileStream(fullFileName, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            string serverFileName = Path.Combine("\\", path, myUniqueFileName);

            return serverFileName;

        }

        public void UpdateTestResultFile(IFormFile medicalTestFile, Visit visit, string webRootPath)
        {
            throw new NotImplementedException();
        }

        //public byte[] GetDocument(string documentPath)
        //{
        //    if (documentPath!=null)
        //    {
        //        if (File.Exists(documentPath))
        //        {
        //                return File.ReadAllBytes(documentPath);
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    return null;

        //}

        public byte[] GetDocument(string documentPath, string webRootPath)
        {
            if (documentPath != null)
            {
                string fullPath = webRootPath + documentPath;// Path.Combine(webRootPath, documentPath);
                if (File.Exists(fullPath))
                {
                    return File.ReadAllBytes(fullPath);
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public void RemoveTestResult(long testResultId, long visitId, string webRootPath)
        {
            Visit visit = PatientMockDB.FutureVisits.Where(c=>c.Id==visitId).First();
            if (visit!=null)
            {
                if (visit.MedicalResult!=null)
                {
                    if (visit.MedicalResult.Id==testResultId)
                    {
                        PatientMockDB.RemoveFile(visit.MedicalResult.DocumentPath);
                        int index = PatientMockDB.MedicalTestResults.FindIndex(c => c.Id == testResultId);
                        PatientMockDB.MedicalTestResults.RemoveAt(index);
                        string fullPath = webRootPath + visit.MedicalResult.DocumentPath;// Path.Combine(webRootPath, documentPath);
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                        else
                        {

                        }
                        visit.MedicalResult = null;

                    }
                }
            }
            //MedicalTestResult medicalTestResult=
        }

        public Person GetPersonById(long personId)
        {
            Person person = PatientMockDB.Persons.First(c => c.Id == personId);
            return person;
        }

        public void AddNotification(long id1, NotificationType testResult, long id2, DateTimeOffset dateTimeOffset, long visitId)
        {
            Notification notification = new Notification();
            notification.EventObjectId = id1;
            notification.PatientId = id2;
            notification.VisitId = visitId;
            notification.Id = PatientMockDB.Notifications.Max(c => c.Id) + 1;
            notification.DateTimeAdded = dateTimeOffset;
            PatientMockDB.Notifications.Add(notification);
        }

        //public void AddPrescription(Recommendation recommendationToAdd)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
