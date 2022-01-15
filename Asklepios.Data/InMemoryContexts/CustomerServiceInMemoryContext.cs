using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.InMemoryContexts
{
    public class CustomerServiceInMemoryContext : ICustomerServiceModuleRepository
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

        public CustomerServiceInMemoryContext()
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

        private Patient GetPatientData()
        {
            CurrentPatient= PatientMockDB.CurrentPatient;
            return CurrentPatient;
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
            return PatientMockDB.AvailableVisits.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Visit> GetAvailableVisits()
        {
            return PatientMockDB.AvailableVisits;
        }

        public Patient GetCurrentPatientData()
        {
            return PatientMockDB.CurrentPatient;
        }

        public Visit GetHistoricalVisitById(long id)
        {
            return PatientMockDB.GetHistoricalVisitById(id);
        }

        public IEnumerable<Visit> GetHistoricalVisits()
        {
            return PatientMockDB.GetHistoricalVisits();
        }

        public Location GetLocationById(long locationId)
        {
            return PatientMockDB.Locations.Where(c=>c.Id==locationId).FirstOrDefault();
        }

        public IEnumerable<Location> GetLocations()
        {
            return PatientMockDB.Locations;
        }

        public MedicalPackage GetMedicalPackageById(long id)
        {
            return PatientMockDB.MedicalPackages.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<MedicalPackage> GetMedicalPackages()
        {
            return PatientMockDB.MedicalPackages;
        }

        public MedicalService GetMedicalServiceById(long id)
        {
            return PatientMockDB.GetMedicalServices().Where(c => c.Id == id).FirstOrDefault(); 
        }

        public IEnumerable<MedicalService> GetMedicalServices()
        {
            return PatientMockDB.GetMedicalServices();
        }

        public MedicalWorker GetMedicalWorkerById(long id)
        {
            return PatientMockDB.GetMedicalWorkerById(id);
        }

        public IEnumerable<MedicalWorker> GetMedicalWorkers()
        {
            return PatientMockDB.MedicalWorkers;
        }

        public NFZUnit GetNFZUnitById(string code)
        {
            return PatientMockDB.NfzUnits.Where(c => c.Code == code).FirstOrDefault();
        }

        public IEnumerable<NFZUnit> GetNFZUnits()
        {
            return PatientMockDB.NfzUnits;
        }

        public Patient GetPatientById(long id)
        {
            return PatientMockDB.AllPatients.Where(c => c.Id == id).FirstOrDefault();
        }

        public Person GetPerson(long personId)
        {
            return PatientMockDB.Persons.Where(c=>c.Id==personId).FirstOrDefault();
        }

        public User GetUser(int parsedId)
        {
            User user= PatientMockDB.GetUserById(parsedId);
            return user;    
        }

        public IEnumerable<VisitCategory> GetVisitCategories()
        {
            return PatientMockDB.VisitCategories;
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
        }

        public void ResignFromVisit(Visit plannedVisit, Patient selectedPatient)
        {
            plannedVisit.Patient = null;
            PatientMockDB.CurrentPatient.BookedVisits.Remove(plannedVisit);
        }

        public void BookVisit(Patient selectedPatient, Visit newVisit)
        {
            newVisit.Patient = selectedPatient;
            PatientMockDB.CurrentPatient.BookedVisits.Add(newVisit);
        }
    }
}
