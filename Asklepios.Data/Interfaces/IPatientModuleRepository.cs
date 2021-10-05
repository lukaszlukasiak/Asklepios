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
        IEnumerable<Location> GetLocations();
        IEnumerable<MedicalWorker> GetMedicalWorkers();
        IEnumerable<Visit> GetHistoricalVisits();
        IEnumerable<MedicalService> GetMedicalServices();
        IEnumerable<MedicalPackage> GetMedicalPackages();
        IEnumerable<NFZUnit> GetNFZUnits();
        IEnumerable<VisitCategory> GetVisitCategories();
    }
}
