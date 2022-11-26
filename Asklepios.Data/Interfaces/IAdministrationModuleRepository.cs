using Asklepios.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Asklepios.Data.Interfaces
{
    public interface IAdministrationModuleRepository
    {
        void AddLocation(Location location);

        void AddMedicalPackage(MedicalPackage newPackage);

        void AddMedicalRoom(MedicalRoom room);

        void AddMedicalWorkerObjects(User user, Person person, MedicalWorker medicalWorker);

        void AddPatientObjects(User user, Person person, Patient patient);

        //List<MedicalServiceDiscount> GetMedicalServiceDiscounts();
        void AddVisitsToSchedule(List<Visit> visitsToAdd);

        Visit FutureVisitById(long id);

        //medicalRooms = GetMedicalRooms().ToList();
        List<Location> GetAllLocations();

        List<Patient> GetAllPatients();

        List<MedicalRoom> GetAllRooms();

        Visit GetAvailableVisitById(long id);

        List<Visit> GetAvailableVisits();

        Patient GetCurrentPatient();

        List<Visit> GetFutureVisits();

        Location GetLocationById(long id);

        MedicalPackage GetMedicalPackageById(long id);

        List<MedicalPackage> GetMedicalPackages();

        MedicalService GetMedicalServiceById(long v);

        List<MedicalService> GetMedicalServices();
        MedicalWorker GetMedicalWorkerById(long id);

        List<MedicalWorker> GetMedicalWorkers();

        NFZUnit GetNFZUnitById(long id);

        List<NFZUnit> GetNFZUnits();

        Patient GetPatientById(long id);

        Person GetPersonById(long personId);

        MedicalRoom GetRoomById(long id);

        List<MedicalRoom> GetRoomsByLocationId();

        List<MedicalRoom> GetUnasignedRooms();

        User GetUserById(long parsedId);

        List<VisitCategory> GetVisitCategories();
        //Location GetLocations(long id);
        VisitCategory GetVisitCategoryById(long id);
        void RemoveLocationById(long selectedLocationId);

        void RemoveMedicalPackageById(long selectedPackageId);

        //void RemoveRoomById(long id);
        void RemoveMedicalRoomById(long selectedRoomId);

        void RemoveMedicalWorkerById(long selectedWorkerId);

        void RemovePatientById(long id);

        void RemoveVisitById(long id);
        void UpdateLocation(Location selectedLocation, long selectedLocationId);

        void UpdateLocationImage(IFormFile imageFile, Location location, string webRootPath);

        void UpdateMedicalPackage(MedicalPackage newPackage);

        void UpdateMedicalWorker(MedicalWorker selectedWorker, long selectedWorkerId);

        void UpdatePatient(Patient patient);
        void UpdatePersonImage(IFormFile imageFile, Person person, string hostEnvironmentPath);
        void UpdateRoom(MedicalRoom newRoom);
    }
}
