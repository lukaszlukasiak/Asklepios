using Asklepios.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Asklepios.Data.Interfaces
{
    public interface IAdministrationModuleRepository
    {
        List<MedicalService> GetMedicalServices();
        List<MedicalPackage> GetMedicalPackages();
        List<Patient> GetAllPatients();
        List<VisitCategory> GetVisitCategories();
        //medicalRooms = GetMedicalRooms().ToList();
        List<Location>  GetAllLocations();
        List<MedicalWorker>  GetMedicalWorkers();
        List<Visit> GetAvailableVisits();
        Patient GetCurrentPatient();
        List<MedicalRoom> GetUnasignedRooms();
        MedicalWorker GetMedicalWorkerById(long id);
        MedicalPackage GetMedicalPackageById(long id);
        Visit GetAvailableVisitById(long id);
        Location GetLocationById(long id);
        //Location GetLocations(long id);
        VisitCategory GetVisitCategoryById(long id);
        Patient GetPatientById(long id);
        User GetUser(int parsedId);
        Person GetPerson(long personId);
        List<MedicalRoom> GetRoomsByLocationId();
        MedicalService GetMedicalServiceById(long v);
        List<MedicalRoom> GetAllRooms();
        List<MedicalServiceDiscount> GetMedicalServiceDiscounts();
        void AddVisitsToSchedule(List<Visit> visitsToAdd);
        void RemoveVisitById(long id);
        List<NFZUnit> GetNFZUnits();
        NFZUnit GetNFZUnitById(long id);
        void AddPatientObjects(User user, Person person, Patient patient);
        void RemovePatientById(long id);
        void UpdatePatient(Patient patient);
        void AddMedicalWorkerObjects(User user, Person person, MedicalWorker medicalWorker);
        void RemoveMedicalWorkerById(long selectedWorkerId);
        void UpdateMedicalWorker(MedicalWorker selectedWorker, long selectedWorkerId);
        void UpdatePersonImage(IFormFile imageFile, Person person, string hostEnvironmentPath);
        void UpdateLocationImage(IFormFile imageFile, Location location, string webRootPath);
        void AddLocation(Location location);
        void UpdateLocation(Location selectedLocation, long selectedLocationId);
        void RemoveLocationById(long selectedLocationId);
        MedicalRoom GetRoomById(long id);
        void AddMedicalRoom(MedicalRoom room);
        //void RemoveRoomById(long id);
        void RemoveMedicalRoomById(long selectedRoomId);
        void UpdateRoom(MedicalRoom newRoom);
        void AddMedicalPackage(MedicalPackage newPackage);
        void RemoveMedicalPackageById(long selectedPackageId);
        void UpdateMedicalPackage(MedicalPackage newPackage);
    }
}
