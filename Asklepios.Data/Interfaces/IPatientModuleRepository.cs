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

        void UpdateReferral(MedicalReferral referral);
        void UpdateVisit(Visit visit);
        //IEnumerable<MedicalRoom> GetMedicalRooms();
        //MedicalRoom GetMedicalRoomById();

    }
}
