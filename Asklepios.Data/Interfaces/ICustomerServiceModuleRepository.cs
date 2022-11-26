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
        IEnumerable<Visit> GetAvailableVisits();
        IEnumerable<Location> GetLocations();
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
        //Patient CurrentPatient { get; set; }
        void UpdateReferral(MedicalReferral referral);
        void UpdateVisit(Visit visit);
        User GetUserById(long parsedId);
        Person GetPersonById(long personId);
        void ResignFromVisit(Visit plannedVisit, Patient selectedPatient);
        void BookVisit(Patient selectedPatient, Visit newVisit);
        List<Visit> GetHistoricalVisitsByPatientId(long id);
        List<Visit> GetBookedVisitsByPatientId(long id);
        List<MedicalReferral> GetMedicalReferralsByPatientId(long id);
    }
}
