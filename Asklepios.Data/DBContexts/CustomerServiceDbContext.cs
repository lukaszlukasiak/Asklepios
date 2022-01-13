using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data
{
    public class CustomerServiceDbContext : DbContext, ICustomerServiceModuleRepository
    {
        public Patient CurrentPatient { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<Location> GetAllLocations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            throw new NotImplementedException();
        }

        public Visit GetAvailableVisitById(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Visit> GetAvailableVisits()
        {
            throw new NotImplementedException();
        }

        public Patient GetCurrentPatientData()
        {
            throw new NotImplementedException();
        }

        public Visit GetHistoricalVisitById(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Visit> GetHistoricalVisits()
        {
            throw new NotImplementedException();
        }

        public Location GetLocationById(long locationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetLocations()
        {
            throw new NotImplementedException();
        }

        public MedicalPackage GetMedicalPackageById(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MedicalPackage> GetMedicalPackages()
        {
            throw new NotImplementedException();
        }

        public MedicalService GetMedicalServiceById(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MedicalService> GetMedicalServices()
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerById(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MedicalWorker> GetMedicalWorkers()
        {
            throw new NotImplementedException();
        }

        public NFZUnit GetNFZUnitById(long id)
        {
            throw new NotImplementedException();
        }

        public NFZUnit GetNFZUnitById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NFZUnit> GetNFZUnits()
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

        public User GetUser(int parsedId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VisitCategory> GetVisitCategories()
        {
            throw new NotImplementedException();
        }

        public VisitCategory GetVisitCategoryById(long id)
        {
            throw new NotImplementedException();
        }

        public void UpdateReferral(MedicalReferral referral)
        {
            throw new NotImplementedException();
        }

        public void UpdateVisit(Visit visit)
        {
            throw new NotImplementedException();
        }
    }
}
