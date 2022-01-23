using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Asklepios.Data
{
    public class AdministrationDbContext : DbContext, IAdministrationModuleRepository
    {
        public void AddVisitsToSchedule(List<Visit> visitsToAdd)
        {
            throw new NotImplementedException();
        }

        public List<Location> GetAllLocations()
        {
            throw new NotImplementedException();
        }

        public List<Patient> GetAllPatients()
        {
            throw new NotImplementedException();
        }

        public Visit GetAvailableVisitById(long id)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetAvailableVisits()
        {
            throw new NotImplementedException();
        }

        public Patient GetCurrentPatient()
        {
            throw new NotImplementedException();
        }

        public Location GetLocationById(long id)
        {
            throw new NotImplementedException();
        }

        public MedicalPackage GetMedicalPackageById()
        {
            throw new NotImplementedException();
        }

        public MedicalPackage GetMedicalPackageById(long id)
        {
            throw new NotImplementedException();
        }

        public List<MedicalPackage> GetMedicalPackages()
        {
            throw new NotImplementedException();
        }

        public MedicalService GetMedicalServiceById(long v)
        {
            throw new NotImplementedException();
        }

        public List<MedicalService> GetMedicalServices()
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerById()
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerById(long id)
        {
            throw new NotImplementedException();
        }

        public List<MedicalWorker> GetMedicalWorkers()
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientById(long id)
        {
            throw new NotImplementedException();
        }

        public Person GetPerson(long personId)
        {
            throw new NotImplementedException();
        }

        public List<MedicalRoom> GetRoomsByLocationId()
        {
            throw new NotImplementedException();
        }

        public User GetUser(int parsedId)
        {
            throw new NotImplementedException();
        }

        public List<VisitCategory> GetVisitCategories()
        {
            throw new NotImplementedException();
        }

        public VisitCategory GetVisitCategoryById(long id)
        {
            throw new NotImplementedException();
        }

        public void RemoveVisitById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
