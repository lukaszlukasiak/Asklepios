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
        void BookVisit(Patient selectedPatient, Visit selectedVisit);

        IEnumerable<Location> GetAllLocations();

        IEnumerable<Patient> GetAllPatients();

        Visit GetAvailableVisitById(long id);

        //Patient GetPatientData();
        IEnumerable<Visit> GetAvailableVisits();
        List<Visit> GetBookedVisitsByPatientId(long id);

        Visit GetHistoricalVisitById(long id);

        List<Visit> GetHistoricalVisitsByPatientId(long id);

        Location GetLocationById(long locationId);

        MedicalPackage GetMedicalPackageById(long id);

        IEnumerable<MedicalPackage> GetMedicalPackages();

        MedicalService GetMedicalServiceById(long id);

        //IEnumerable<Visit> GetHistoricalVisits();
        IEnumerable<MedicalService> GetMedicalServices();

        MedicalWorker GetMedicalWorkerById(long id);

        IEnumerable<MedicalWorker> GetMedicalWorkers();
        NFZUnit GetNFZUnitById(long id);

        IEnumerable<NFZUnit> GetNFZUnits();
        Notification GetNotificationById(long id);

        List<Notification> GetNotificationsByPatientId(long id);

        Patient GetPatientById(long id);

        Patient GetPatientByUserId(long userId);

        //Patient CurrentPatient { get; set; }
        User GetUserById(long parsedId);

        IEnumerable<VisitCategory> GetVisitCategories();
        VisitCategory GetVisitCategoryById(long id);
        void ResignFromVisit(long id);

        void UpdateReferral(MedicalReferral referral);
        void UpdateVisit(Visit visit);
        //void ResignFromVisit(Visit plannedVisit, Patient patient);

        //IEnumerable<MedicalRoom> GetMedicalRooms();
        //MedicalRoom GetMedicalRoomById();

    }
}
