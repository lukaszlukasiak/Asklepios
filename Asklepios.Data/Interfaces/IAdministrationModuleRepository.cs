using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.Interfaces
{
    public interface IAdministrationModuleRepository
    {
        Patient GetCurrentPatient();
        MedicalWorker GetMedicalWorker();
        MedicalPackage GetMedicalPackage();
        
    }
}
