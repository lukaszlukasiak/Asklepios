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
        List<Visit> GetAvailableVisits();
        List<Location> GetLocations();
        List<MedicalWorker> GetMedicalWorkers();
        List<Visit> GetHistoricalVisits();
        List<MedicalService> GetMedicalServices();
        List<MedicalPackage> GetMedicalPackages();
        List<NFZUnit> GetNFZUnits();
        List<VisitCategory> GetVisitCategories();
        List<Location> GetAllLocations();
        List<Patient> GetAllPatients();
        Patient GetPatientById(long id);
        Location GetLocationById(long locationId);
        Visit GetAvailableVisitById(long id);
        MedicalWorker GetMedicalWorkerById(long id);
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
        List<Visit> GetHistoricalVisitsByPatientId(long id);
        List<Visit> GetBookedVisitsByPatientId(long id);
        List<MedicalReferral> GetMedicalReferralsByPatientId(long id);
    }
}
