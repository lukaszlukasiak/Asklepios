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
        Visit GetAvailableVisitById();
        MedicalWorker GetMedicalWorkerById();
        Visit GetHistoricalVisitById();
        MedicalService GetMedicalServiceById();
        MedicalPackage GetMedicalPackageById();
        NFZUnit GetNFZUnitById();
        VisitCategory GetVisitCategoryById();
        //IEnumerable<MedicalRoom> GetMedicalRooms();
        //MedicalRoom GetMedicalRoomById();

    }
}
