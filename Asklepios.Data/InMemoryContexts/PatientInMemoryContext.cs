using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Asklepios.Data.InMemoryContexts
{
    public class PatientInMemoryContext : IPatientModuleRepository
    {


        readonly IEnumerable<Visit> availableVisits;
        //readonly IEnumerable<Visit> historicalVisits;

        readonly IEnumerable<Location> locations;
        //public  Patient Patient { get; set; }
        private readonly IEnumerable<MedicalWorker> medicalWorkers;
        public PatientInMemoryContext()
        {
            if (!PatientMockDB.IsCreated)
            {
                PatientMockDB.SetData();
            }
            nfzUnits = GetNFZUnits().ToList();
            medicalServices = GetMedicalServices().ToList();
            medicalPackages = GetMedicalPackages().ToList();
            allPatients = GetAllPatients().ToList();
            primaryMedicalServices = medicalServices.Where(c => c.IsPrimaryService == true).ToList();
            visitCategories = GetVisitCategories().ToList();
            //medicalRooms = GetMedicalRooms().ToList();
            locations = GetAllLocations();
            medicalWorkers = GetMedicalWorkers();
            availableVisits = GetAvailableVisits().Where(c => c.Patient == null).ToList(); ;
            //CurrentPatient = GetPatientData();
        }

        private List<Patient> allPatients { get; set; }
        private List<MedicalPackage> medicalPackages { get; set; }
        private List<List<MedicalRoom>> medicalRooms { get; set; }
        private List<MedicalService> medicalServices { get; set; }
        private List<NFZUnit> nfzUnits { get; set; }
        private List<MedicalService> primaryMedicalServices { get; set; }
        private List<VisitCategory> visitCategories { get; set; }
        //public Patient CurrentPatient { get; set; }
        //static string lol;
        //public Patient GetPatientData()
        //{
        //    return PatientMockDB.CurrentPatient;
        //}

        public void BookVisit(Patient selectedPatient, Visit selectedVisit)
        {
            selectedVisit.Patient = selectedPatient;
            //selectedPatient.BookedVisits.Add(selectedVisit);
            //PatientMockDB.BookedVisits.Add(selectedVisit);
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return PatientMockDB.Locations;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return PatientMockDB.AllPatients;
        }

        public Visit GetAvailableVisitById(long id)
        {
            return GetAvailableVisits().Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Visit> GetAvailableVisits()
        {
            return PatientMockDB.AvailableVisits;
        }

        public List<Visit> GetBookedVisitsByPatientId(long id)
        {
            List<Visit> visits = PatientMockDB.BookedVisits.Where(c => c.Patient.Id == id).ToList();
            return visits;
        }

        public Visit GetHistoricalVisitById(long id)
        {
            return PatientMockDB.GetHistoricalVisitById(id);
        }

        public List<Visit> GetHistoricalVisitsByPatientId(long id)
        {
            List<Visit> visits = PatientMockDB.HistoricalVisits.Where(c => c.Patient.Id == id).ToList();
            return visits;
        }

        public Location GetLocationById(long locationId)
        {
            return PatientMockDB.GetLocationById(locationId);
        }

        public MedicalPackage GetMedicalPackageById(long id)
        {
            return PatientMockDB.GetMedicalPackageById(id);
        }

        public IEnumerable<MedicalPackage> GetMedicalPackages()
        {
            return PatientMockDB.MedicalPackages;
        }

        public MedicalService GetMedicalServiceById(long id)
        {
            return PatientMockDB.GetMedicalServiceById(id);
        }

        public IEnumerable<MedicalService> GetMedicalServices()
        {
            return PatientMockDB.MedicalServices;
        }

        public MedicalWorker GetMedicalWorkerById(long id)
        {
            return PatientMockDB.GetMedicalWorkerById(id);
        }

        public IEnumerable<MedicalWorker> GetMedicalWorkers()
        {
            return PatientMockDB.MedicalWorkers;
        }

        public NFZUnit GetNFZUnitById(long id)
        {
            return PatientMockDB.GetNFZUnitById(id);
        }

        //public IEnumerable<Visit> GetHistoricalVisits(long patientId)
        //{
        //    GetHistoricalVisitsByPatientId
        //    return PatientMockDB.CurrentPatient.HistoricalVisits;
        //}
        public IEnumerable<NFZUnit> GetNFZUnits()
        {
            return PatientMockDB.NfzUnits;
        }

        public Notification GetNotificationById(long id)
        {
            Notification notification = PatientMockDB.Notifications.Where(c => c.Id == id).FirstOrDefault();
            return notification;
        }

        public List<Notification> GetNotificationsByPatientId(long id)
        {
            List<Notification> notifications = PatientMockDB.Notifications.Where(c => c.PatientId == id).ToList();
            return notifications;
        }

        public Patient GetPatientById(long id)
        {
            return PatientMockDB.GetPatientById(id);
        }

        public Patient GetPatientByUserId(long userId)
        {
            Patient patient = PatientMockDB.AllPatients.Where(c => c.User.Id == userId).FirstOrDefault();
            return patient;
        }

        public User GetUserById(long parsedId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VisitCategory> GetVisitCategories()
        {
            return PatientMockDB.VisitCategories;
        }
        public VisitCategory GetVisitCategoryById(long id)
        {
            return PatientMockDB.GetVisitCategoryById(id);
        }

        public void ResignFromVisit(long plannedVisitId)
        {
            Visit visit = PatientMockDB.FutureVisits.First(c => c.Id == plannedVisitId);
            if (visit != null)
            {
                visit.Patient = null;
                visit.UsedExaminationReferral = null;
                visit.VisitStatus = Core.Enums.VisitStatus.AvailableNotBooked;
            }
            //patient.BookedVisits.Remove(plannedVisit);
            //PatientMockDB.BookedVisits.Remove(plannedVisit);
        }

        public void UpdateReferral(MedicalReferral referral)
        {
            MedicalReferral refe = PatientMockDB.MedicalReferrals.Where(c => c.Id == referral.Id).FirstOrDefault();
            refe = referral;
        }

        public void UpdateVisit(Visit visit)
        {
            Visit oldVisit = PatientMockDB.AvailableVisits.Where(c => c.Id == visit.Id).FirstOrDefault();
            oldVisit = visit;
            //PatientMockDB.CurrentPatient.BookedVisits.Add(visit);
            //PatientMockDB.AvailableVisits.
            //throw new NotImplementedException();
        }
    }
}
