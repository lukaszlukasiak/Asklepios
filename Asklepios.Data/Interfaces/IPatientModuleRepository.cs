using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.Interfaces
{
    public interface IPatientModuleRepository
    {
        Patient GetPatientData();
        IEnumerable<Visit> GetAvailableVisits();
        IEnumerable<MedicalWorker> GetMedicalWorkers();
        IEnumerable<Visit> GetHistoricalVisits();
        IEnumerable<MedicalService> GetMedicalServices();
        IEnumerable<MedicalPackage> GetMedicalPackages();
        IEnumerable<NFZUnit> GetNFZUnits();
        IEnumerable<VisitCategory> GetVisitCategories();
        IEnumerable<Location> GetAllLocations();
        IEnumerable<Patient> GetAllPatients();
        Patient GetPatientById(long id);
        Location GetLocationById(long locationId);
        Visit GetAvailableVisitById(long id);
        MedicalWorker GetMedicalWorkerById(long id);
        Visit GetHistoricalVisitById(long id);
        MedicalService GetMedicalServiceById(long id);
        MedicalPackage GetMedicalPackageById(long id);
        NFZUnit GetNFZUnitById(long id);
        VisitCategory GetVisitCategoryById(long id);
        Patient CurrentPatient { get; set; }
        User GetUser(int parsedId);

        void UpdateReferral(MedicalReferral referral);
        Patient GetPatientByUserId(long personId);
        void UpdateVisit(Visit visit);
        void BookVisit(Patient selectedPatient, Visit selectedVisit);
        List<Notification> GetNotificationsByPatientId(long id);
        List<Visit> GetHistoricalVisitsByPatientId(long id);
        List<Visit> GetBookedVisitsByPatientId(long id);
        Notification GetNotificationById(long id);
        void ResignFromVisit(long id);
        //void ResignFromVisit(Visit plannedVisit, Patient patient);

        //IEnumerable<MedicalRoom> GetMedicalRooms();
        //MedicalRoom GetMedicalRoomById();

    }
}
