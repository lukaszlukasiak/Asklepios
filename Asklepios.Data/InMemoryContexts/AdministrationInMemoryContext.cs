using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asklepios.Data.InMemoryContexts
{
    public class AdministrationInMemoryContext : IAdministrationModuleRepository
    {
        readonly IEnumerable<Visit> availableVisits;
        readonly IEnumerable<Location> locations;
        private readonly IEnumerable<MedicalWorker> medicalWorkers;
        private List<MedicalService> medicalServices { get; set; }
        private List<MedicalService> primaryMedicalServices { get; set; }
        private List<VisitCategory> visitCategories { get; set; }
        private List<MedicalPackage> medicalPackages { get; set; }
        private List<NFZUnit> nfzUnits { get; set; }
        private List<Patient> allPatients { get; set; }
        private List<List<MedicalRoom>> medicalRooms { get; set; }

        public AdministrationInMemoryContext()
        {
            if (!PatientMockDB.IsCreated)
            {
                PatientMockDB.SetData();
            }
            //nfzUnits = GetNFZUnits().ToList();
            medicalServices = GetMedicalServices().ToList();
            medicalPackages = GetMedicalPackages().ToList();
            allPatients = GetAllPatients().ToList();
            primaryMedicalServices = medicalServices.Where(c => c.IsPrimaryService == true).ToList();
            visitCategories = GetVisitCategories().ToList();
            //medicalRooms = GetMedicalRooms().ToList();
            locations = GetAllLocations();
            medicalWorkers = GetMedicalWorkers();
            availableVisits = GetAvailableVisits().Where(c => c.Patient == null).ToList(); ;
        }
        public Patient GetCurrentPatient()
        {
            throw new NotImplementedException();
        }


        public Person GetPerson(long personId)
        {
            return PatientMockDB.Persons.Where(c => c.Id == personId).FirstOrDefault();
        }

        public User GetUser(int parsedId)
        {
            User user = PatientMockDB.GetUserById(parsedId);
            return user;
        }

        public List<MedicalService> GetMedicalServices()
        {
            return PatientMockDB.MedicalServices;
        }

        public List<MedicalPackage> GetMedicalPackages()
        {
            return PatientMockDB.MedicalPackages;
        }

        public List<Patient> GetAllPatients()
        {
            return PatientMockDB.AllPatients;
        }

        public List<VisitCategory> GetVisitCategories()
        {
            return PatientMockDB.VisitCategories;
        }

        public List<Location> GetAllLocations()
        {
            return PatientMockDB.Locations.ToList();
        }

        public List<MedicalWorker> GetMedicalWorkers()
        {
            return PatientMockDB.MedicalWorkers.ToList();
        }

        public List<Visit> GetAvailableVisits()
        {
            return PatientMockDB.AvailableVisits.ToList();
        }

        public MedicalPackage GetMedicalPackageById(long id)
        {
            return PatientMockDB.MedicalPackages.Where(c => c.Id == id).FirstOrDefault();
        }

        public MedicalWorker GetMedicalWorkerById(long id)
        {
            return PatientMockDB.GetMedicalWorkerById(id);
        }

        public Visit GetAvailableVisitById(long id)
        {
            return PatientMockDB.AvailableVisits.Where(c => c.Id == id).FirstOrDefault();
        }

        public Location GetLocationById(long id)
        {
            return PatientMockDB.Locations.Where(c => c.Id == id).FirstOrDefault();
        }

        public VisitCategory GetVisitCategoryById(long id)
        {
            return PatientMockDB.VisitCategories.Where(c => c.Id == id).FirstOrDefault();
        }

        public Patient GetPatientById(long id)
        {
            return PatientMockDB.AllPatients.Where(c => c.Id == id).FirstOrDefault();
        }

        public List<MedicalRoom> GetRoomsByLocationId()
        {
            throw new NotImplementedException();
        }

        public MedicalService GetMedicalServiceById(long id)
        {
            return PatientMockDB.GetMedicalServiceById(id);
        }

        public void AddVisitsToSchedule(List<Visit> visitsToAdd)
        {
            foreach (Visit item in visitsToAdd)
            {
                PatientMockDB.AvailableVisits.Append(item);
            }          
        }

        public void RemoveVisitById(long id)
        {
            PatientMockDB.RemoveVisitById(id);
        }
    }
}
