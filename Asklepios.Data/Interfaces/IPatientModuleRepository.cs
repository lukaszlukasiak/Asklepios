using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.Interfaces
{
    public interface IPatientModuleRepository
    {
        void BookVisit(long patientId, long visitId);

        List<Location> GetAllLocations();

        IQueryable<Patient> GetAllPatients();

        Visit GetFutureVisitById(long id);

        //Patient GetPatientData();
        IQueryable<Visit> GetAvailableVisitsQuery();
        IQueryable<Visit> GetHistoricalVisitsByPatientIdQuery(long id);
        IQueryable<Visit> GetBookedVisitsByPatientIdQuery(long id);

        List<Visit> GetBookedVisitsByPatientId(long id);

        Visit GetHistoricalVisitById(long id);

        Location GetLocationById(long locationId);

        MedicalPackage GetMedicalPackageById(long id);

        List<MedicalPackage> GetMedicalPackages();

        MedicalService GetMedicalServiceById(long id);

        //IEnumerable<Visit> GetHistoricalVisits();
        List<MedicalService> GetMedicalServices();

        MedicalWorker GetMedicalWorkerById(long id);

        List<MedicalWorker> GetMedicalWorkers();
        NFZUnit GetNFZUnitById(long id);

        List<NFZUnit> GetNFZUnits();
        Notification GetNotificationById(long id);

        List<Notification> GetNotificationsByPatientId(long id);

        Patient GetPatientById(long id);

        Patient GetPatientByUserId(long userId);

        //Patient CurrentPatient { get; set; }
        User GetUserById(long parsedId);

        List<VisitCategory> GetVisitCategories();
        VisitCategory GetVisitCategoryById(long id);
        void ResignFromVisit(long id);

        void UpdateReferral(MedicalReferral referral);
        void UpdateVisit(Visit visit);
        //IQueryable<Visit> GetAvailableVisitsQuery();
        IQueryable<Visit> GetAllVisitsByPatientIdQuery(long id);
        IQueryable<MedicalReferral> GetMedicalReferralsByPatientIdQuery(long id);
        IQueryable<MedicalTestResult> GetMedicalTestResultsByPatientIdQuery(long id);
        IQueryable<Prescription> GetPrescriptionsByPatientIdQuery(long id);
        MedicalTestResult GetMedicalTestResultById(long idL);
        byte[] GetDocument(string documentPath, string webRootPath);
        void UpdateNotification(Notification notification);
        IQueryable<Visit> GetFutureVisitsQuery();
        IQueryable<Visit> GetFutureVisitsQueryPatient();

        //void ResignFromVisit(Visit plannedVisit, Patient patient);

        //IEnumerable<MedicalRoom> GetMedicalRooms();
        //MedicalRoom GetMedicalRoomById();

    }
}
