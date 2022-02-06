using Asklepios.Core.Models;
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
        MedicalWorker GetMedicalWorkerById(long id);
        MedicalPackage GetMedicalPackageById(long id);
        Visit GetAvailableVisitById(long id);
        Location GetLocationById(long id);
        VisitCategory GetVisitCategoryById(long id);
        Patient GetPatientById(long id);
        User GetUser(int parsedId);
        Person GetPerson(long personId);
        List<MedicalRoom> GetRoomsByLocationId();
        MedicalService GetMedicalServiceById(long v);
        void AddVisitsToSchedule(List<Visit> visitsToAdd);
        void RemoveVisitById(long id);
        List<NFZUnit> GetNFZUnits();
        NFZUnit GetNFZUnitById(long id);
        void AddPatientObjects(User user, Person person, Patient patient);
        void RemovePatientById(long id);
        void UpdatePatient(Patient patient);
    }
}
