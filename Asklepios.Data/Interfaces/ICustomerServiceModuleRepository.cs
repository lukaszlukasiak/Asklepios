using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.Interfaces
{
    public interface ICustomerServiceModuleRepository
    {
        //Patient GetCurrentPatientData();
        IQueryable<Visit> GetAvailableVisitsQuery();
        List<Location> GetLocations();
        List<MedicalWorker> GetMedicalWorkers();
        IQueryable<Visit> GetHistoricalVisitsQuery();
        List<MedicalService> GetMedicalServices();
        List<MedicalPackage> GetMedicalPackages();
        List<NFZUnit> GetNFZUnits();
        List<VisitCategory> GetVisitCategories();
        List<Location> GetAllLocations();
        IQueryable<Patient> GetAllPatients();
        Patient GetPatientById(long id);
        Location GetLocationById(long locationId);
        Visit GetFutureVisitById(long id);
        MedicalWorker GetMedicalWorkerById(long id);
        MedicalWorker GetMedicalWorkerDetailsById(long id);

        Visit GetHistoricalVisitById(long id);
        MedicalService GetMedicalServiceById(long id);
        MedicalPackage GetMedicalPackageById(long id);
        NFZUnit GetNFZUnitById(long id);
        VisitCategory GetVisitCategoryById(long id);
        //Patient CurrentPatient { get; set; }
        void UpdateReferral(MedicalReferral referral);
        void UpdateVisit(Visit visit);
        User GetUserById(long parsedId);
        Person GetPersonById(long personId);
        void ResignFromVisit(long visitId);
        void BookVisit(long patientId, long visitId);
        IQueryable<Visit> GetHistoricalVisitsByPatientIdQuery(long id);
        List<Visit> GetBookedVisitsByPatientId(long id);
        IQueryable<MedicalReferral> GetMedicalReferralsByPatientIdQuery(long id);
        List<Prescription> GetPrescriptionsByPatientId(long id);
        List<MedicalTestResult> GetTestResultsByPatientId(long id);
        MedicalTestResult GetMedicalTestResultById(long idL);
        byte[] GetDocument(string documentPath, string webRootPath);
        IQueryable<Visit> GetBookedVisitsByPatientIdQuery(long id);
    }
}
