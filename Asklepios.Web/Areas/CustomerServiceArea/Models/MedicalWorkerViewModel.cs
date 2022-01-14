using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public class MedicalWorkerViewModel:BaseViewModel
    {
        public MedicalWorker MedicalWorker { get; set; }

        public MedicalWorkerViewModel(Patient patient, MedicalWorker medicalWorker)
        {
            SelectedPatient = patient;
            MedicalWorker = medicalWorker;
        }
    }
}
