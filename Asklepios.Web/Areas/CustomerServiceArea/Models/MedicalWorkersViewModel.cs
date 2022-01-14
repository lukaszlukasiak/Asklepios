using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public class MedicalWorkersViewModel:BaseViewModel
    {
        public List<MedicalWorker> MedicalWorkers { get; set; }

        public MedicalWorkersViewModel(Patient patient, List<MedicalWorker> medicalWorkers)
        {
            SelectedPatient = patient;
            MedicalWorkers = medicalWorkers;
        }
    }
}
