using Asklepios.Core.Models;

namespace Asklepios.Data.Interfaces
{
    public interface IAdministrationModuleRepository
    {
        Patient GetCurrentPatient();
        MedicalWorker GetMedicalWorker();
        MedicalPackage GetMedicalPackage();
        
    }
}
