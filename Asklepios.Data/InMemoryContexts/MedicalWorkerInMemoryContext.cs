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
        public IQueryable<Visit> GetFutureVisitsByMedicalWorkerId(long id)
        {
            IQueryable<Visit> visits = PatientMockDB.FutureVisits.Where(c => c.MedicalWorker.Id == id).AsQueryable();
            return visits;
        }

        public IQueryable<Visit> GetHistoricalVisitsByMedicalWorkerId(long id)
        {
            IQueryable<Visit> visits = PatientMockDB.HistoricalVisits.Where(c => c.MedicalWorker.Id == id).AsQueryable();
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

        public Patient GetPatientById(long id)
        {
            Patient patient = PatientMockDB.GetPatientById(id);
            return patient;
        }

        public List<VisitReview> GetReviewsByMedicalWorkerId(long id)
        {
            IQueryable<Visit> visits = GetHistoricalVisitsByMedicalWorkerId(id);
            if (visits != null)
            {
                return visits.Where(c => c.VisitReview != null).Select(c => c.VisitReview).ToList();
            }
            else
            {
                return null;
            }
        }

        public Visit GetFutureVisitById(long currentVisitId)
        {           
            Visit visit = PatientMockDB.GetAvailableVisitById(currentVisitId);
            return visit;
        }
        public Visit GetHistoricalVisitById(long currentVisitId)
        {
            Visit visit = PatientMockDB.GetHistoricalVisitById(currentVisitId);
            return visit;
        }

        public MedicalWorker GetMedicalWorkerById(long id)
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
        public void UpdatePrescription(Prescription prescription)
        {

        }
        public void UpdateVisit(Visit visit)
        {
            //int index=
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
                if (visit.MedicalTestResult!=null)
                {
                    if (visit.MedicalTestResult.Id==testResultId)
                    {
                        PatientMockDB.RemoveFile(visit.MedicalTestResult.DocumentPath);
                        int index = PatientMockDB.MedicalTestResults.FindIndex(c => c.Id == testResultId);
                        PatientMockDB.MedicalTestResults.RemoveAt(index);
                        string fullPath = webRootPath + visit.MedicalTestResult.DocumentPath;// Path.Combine(webRootPath, documentPath);
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                        else
                        {

                        }
                        visit.MedicalTestResult = null;

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
            Notification notification = new Notification
            {
                EventObjectId = id1,
                PatientId = id2,
                VisitId = visitId,
                Id = PatientMockDB.Notifications.Max(c => c.Id) + 1,
                DateTimeAdded = dateTimeOffset
            };
            PatientMockDB.Notifications.Add(notification);
        }

        public IQueryable<Visit> GetVisitsByMedicalWorkerId(long id)
        {
            return PatientMockDB.AllVisits.Where(c=>c.MedicalWorkerId==id).AsQueryable();
        }

        public Visit GetVisitById(long id)
        {
            Visit visit = PatientMockDB.AllVisits.Where(c => c.Id == id).FirstOrDefault();
            return visit;
        }

        public MedicalRoom GetMedicalRoomById(long medicalRoomId)
        {
            MedicalRoom medicalRoom = PatientMockDB.MedicalRooms.Where(c => c.Id == medicalRoomId).FirstOrDefault();
            return medicalRoom;
        }

        public VisitCategory GetVisitCategoryById(long visitCategoryId)
        {
            throw new NotImplementedException();
        }

        public MedicalPackage GetMedicalPackageById(long medicalPackageId)
        {
            MedicalPackage medicalPackage = PatientMockDB.MedicalPackages.Where(c => c.Id == medicalPackageId).FirstOrDefault();
            return medicalPackage;
        }

        public User GetUserById(long userId)
        {
            User user = PatientMockDB.Users.Where(c => c.Id == userId).FirstOrDefault();
            return user;
        }

        public NFZUnit GetNFZUnitById(long nFZUnitId)
        {
            //if (nFZUnitId.HasValue)
            //{
                NFZUnit unit = PatientMockDB.NfzUnits.Where(c => c.Id == nFZUnitId).FirstOrDefault();
                return unit;

            //}
            //else
            //{
            //    return null;
            //}
        }

        public Location GetLocationById(long locationId)
        {
            //if (locationId.HasValue)
            //{
                Location location = PatientMockDB.Locations.Where(c => c.Id == locationId).FirstOrDefault();
                return location;
            //}
            //else
            //{
            //    return null;
            //}
        }

        public Visit GetBookedVisitByIdANT(long currentVisitId)
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerByUserId(long id)
        {
            throw new NotImplementedException();
        }

        public MedicalTestResult GetMedicalTestResultById(long idL)
        {
            throw new NotImplementedException();
        }

        public List<MedicalReferral> GetMedicalReferralsByPatientId(long id)
        {
            throw new NotImplementedException();
        }

        public List<MedicalTestResult> GetMedicalTestResultsByPatientId(long id)
        {
            throw new NotImplementedException();
        }

        public List<Prescription> GetPrescriptionsByPatientId(long id)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetHistoricalVisitsByPatientId(long id)
        {
            throw new NotImplementedException();
        }

        public string SaveFile(IFormFile formFile, StorageFolderType type, string basePath)
        {
            throw new NotImplementedException();
        }

        public void RemoveFile(string v)
        {
            throw new NotImplementedException();
        }

        public void RemoveFile(string v, string webRootPath)
        {
            throw new NotImplementedException();
        }



        //public void AddPrescription(Recommendation recommendationToAdd)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
