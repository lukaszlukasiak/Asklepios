using Asklepios.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Asklepios.Data.Interfaces
{
    public interface IAdministrationModuleRepository
    {
        void AddLocation(Location location, IFormFile file, string webRootPath);

        void AddMedicalPackage(MedicalPackage newPackage);

        void AddMedicalRoom(MedicalRoom room);

        void AddMedicalWorkerObjects(MedicalWorker medicalWorker, string webRootPath);

        void AddPatientObjects(Patient patient, string webRootPath);

        //List<MedicalServiceDiscount> GetMedicalServiceDiscounts();
        void AddVisitsToSchedule(List<Visit> visitsToAdd);

        Visit FutureVisitById(long id);
        bool HasMedicalWorkerVisits(long id);
        bool HasPatientVisits(long id);
        //medicalRooms = GetMedicalRooms().ToList();
        List<Location> GetAllLocations();

        IQueryable<Patient> GetAllPatients();

        List<MedicalRoom> GetAllRooms();

        Visit GetFutureVisitById(long id);

        IQueryable<Visit> GetAvailableVisitsQuery();


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

        List<MedicalRoom> GetRoomsByLocationId(long id);

        //List<MedicalRoom> GetUnasignedRooms();

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
        void UpdateLocation(Location selectedLocation, string webrootPath);

        //void UpdateLocationImage(IFormFile imageFile, Location location, string webRootPath);

        void UpdateMedicalPackage(MedicalPackage newPackage);
        void UpdateMedicalWorker(MedicalWorker selectedWorker, string webrootPath);
        void UpdatePatient(Patient patient, string webrootPath);
        void UpdatePersonImage(IFormFile imageFile, Person person, string hostEnvironmentPath);
        void UpdateRoom(MedicalRoom newRoom);
        List<Visit> GetFutureVisitsChunk(int currentPageNumId, int itemsPerPage);
        MedicalWorker GetMedicalWorkerDetailsById(long id    );
        IQueryable<Visit> GetFutureVisitsQuery();
    }
}
