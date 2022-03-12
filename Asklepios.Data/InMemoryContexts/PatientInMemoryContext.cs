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
        private List<MedicalService> medicalServices { get; set; }
        private List<MedicalService> primaryMedicalServices { get; set; }
        private List<VisitCategory> visitCategories { get; set; }
        private List<MedicalPackage> medicalPackages { get; set; }
        private List<NFZUnit> nfzUnits { get; set; }
        private List<Patient> allPatients { get; set; }
        private List<List<MedicalRoom>> medicalRooms { get; set; }
        public Patient CurrentPatient { get; set; }
        //static string lol;

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
            CurrentPatient = GetPatientData();
        }

        public Patient GetPatientData()
        {
            return PatientMockDB.CurrentPatient;
        }

        public IEnumerable<Visit> GetAvailableVisits()
        {
            return PatientMockDB.AvailableVisits;
        }

        public IEnumerable<MedicalWorker> GetMedicalWorkers()
        {
            return PatientMockDB.MedicalWorkers;
        }

        public IEnumerable<Visit> GetHistoricalVisits()
        {
            return PatientMockDB.CurrentPatient.HistoricalVisits;
        }

        public IEnumerable<MedicalService> GetMedicalServices()
        {
            return PatientMockDB.MedicalServices;
        }

        public IEnumerable<MedicalPackage> GetMedicalPackages()
        {
            return PatientMockDB.MedicalPackages;
        }

        public IEnumerable<NFZUnit> GetNFZUnits()
        {
            return PatientMockDB.NfzUnits;
        }

        public IEnumerable<VisitCategory> GetVisitCategories()
        {
            return PatientMockDB.VisitCategories;
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return PatientMockDB.Locations;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return PatientMockDB.AllPatients;
        }

        public Patient GetPatientById(long id)
        {
            return PatientMockDB.GetPatientById(id);
        }

        public Location GetLocationById(long locationId)
        {
            return PatientMockDB.GetLocationById(locationId);
        }

        public Visit GetAvailableVisitById(long id)
        {
            return GetAvailableVisits().Where(c => c.Id == id).FirstOrDefault();
        }

        public MedicalWorker GetMedicalWorkerById(long id)
        {
            return PatientMockDB.GetMedicalWorkerById(id);
        }

        public Visit GetHistoricalVisitById(long id)
        {
            return PatientMockDB.GetHistoricalVisitById(id);
        }

        public MedicalService GetMedicalServiceById(long id)
        {
            return PatientMockDB.GetMedicalServiceById(id);
        }

        public MedicalPackage GetMedicalPackageById(long id)
        {
            return PatientMockDB.GetMedicalPackageById(id);
        }

        public NFZUnit GetNFZUnitById(long id)
        {
            return PatientMockDB.GetNFZUnitById(id);
        }

        public VisitCategory GetVisitCategoryById(long id)
        {
            return PatientMockDB.GetVisitCategoryById(id);
        }

        public void UpdateReferral(MedicalReferral referral)
        {
            MedicalReferral refe = PatientMockDB.CurrentPatient.MedicalReferrals.Where(c => c.Id == referral.Id).FirstOrDefault();
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
        public User GetUser(int parsedId)
        {
            User user = PatientMockDB.GetUserById(parsedId);
            return user;
        }

        public void ResignFromVisit(Visit plannedVisit, Patient patient)
        {
            plannedVisit.Patient = null;
            //patient.BookedVisits.Remove(plannedVisit);
            PatientMockDB.CurrentPatient.BookedVisits.Remove(plannedVisit);
        }

        public void BookVisit(Patient selectedPatient, Visit selectedVisit)
        {
            selectedVisit.Patient = selectedPatient;
            //selectedPatient.BookedVisits.Add(selectedVisit);
            PatientMockDB.CurrentPatient.BookedVisits.Add(selectedVisit);
        }

        public Patient GetPatientByUserId(long userId)
        {
            Patient patient = PatientMockDB.AllPatients.Where(c => c.User.Id == userId).FirstOrDefault();
            return patient;
        }
    }
}
