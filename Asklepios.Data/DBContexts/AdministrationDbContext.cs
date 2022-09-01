using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Asklepios.Data
{
    public class AdministrationDbContext : DbContext, IAdministrationModuleRepository
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalWorker> MedicalWorkers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<MedicalRoom> MedicalRooms { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<MedicalPackage> MedicalPackages{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> People{ get; set; }

        public AdministrationDbContext()
        {

        }

        // Metoda pozwala na wskazanie i konfigurację źródła danych
        // Przykład użycia był doskonale widoczny w poprzednim wpisie
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        // Metoda pozwala na konfigurację modelu przy wykorzystaniu Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public void AddLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public void AddMedicalPackage(MedicalPackage newPackage)
        {
            throw new NotImplementedException();
        }

        public void AddMedicalRoom(MedicalRoom room)
        {
            throw new NotImplementedException();
        }

        public void AddMedicalWorkerObjects(User user, Person person, MedicalWorker medicalWorker)
        {
            throw new NotImplementedException();
        }

        public void AddPatientObjects(User user, Person person, Patient patient)
        {
            throw new NotImplementedException();
        }

        public void AddVisitsToSchedule(List<Visit> visitsToAdd)
        {
            throw new NotImplementedException();
        }

        public Visit FutureVisitById(long id)
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

        public List<MedicalRoom> GetAllRooms()
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

        public List<Visit> GetFutureVisits()
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

        public List<MedicalServiceDiscount> GetMedicalServiceDiscounts()
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

        public NFZUnit GetNFZUnitById(long id)
        {
            throw new NotImplementedException();
        }

        public List<NFZUnit> GetNFZUnits()
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

        public MedicalRoom GetRoomById(long id)
        {
            throw new NotImplementedException();
        }

        public List<MedicalRoom> GetRoomsByLocationId()
        {
            throw new NotImplementedException();
        }

        public List<MedicalRoom> GetUnasignedRooms()
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

        public void RemoveLocationById(long selectedLocationId)
        {
            throw new NotImplementedException();
        }

        public void RemoveMedicalPackageById(long selectedPackageId)
        {
            throw new NotImplementedException();
        }

        public void RemoveMedicalRoomById(long selectedRoomId)
        {
            throw new NotImplementedException();
        }

        public void RemoveMedicalWorkerById(long selectedWorkerId)
        {
            throw new NotImplementedException();
        }

        public void RemovePatientById(long id)
        {
            throw new NotImplementedException();
        }

        //public void RemoveRoomById(long id)
        //{
        //    throw new NotImplementedException();
        //}

        public void RemoveVisitById(long id)
        {
            throw new NotImplementedException();
        }

        public void UpdateLocation(Location selectedLocation, long selectedLocationId)
        {
            throw new NotImplementedException();
        }

        public void UpdateLocationImage(IFormFile imageFile, Location location, string webRootPath)
        {
            throw new NotImplementedException();
        }

        public void UpdateMedicalPackage(MedicalPackage newPackage)
        {
            throw new NotImplementedException();
        }

        public void UpdateMedicalWorker(MedicalWorker selectedWorker, long selectedWorkerId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public void UpdatePersonImage(IFormFile imageFile, Person person)
        {
            throw new NotImplementedException();
        }

        public void UpdatePersonImage(IFormFile imageFile, Person person, string hostEnvironmentPath)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoom(MedicalRoom newRoom)
        {
            throw new NotImplementedException();
        }
    }
}
