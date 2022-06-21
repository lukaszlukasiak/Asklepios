using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class MedicalWorkerViewModel:IBaseViewModel
    {
        public MedicalWorkerViewModel(MedicalWorker worker)
        {
            MedicalWorker = worker;
        }

        public MedicalWorker MedicalWorker { get; set; }
        public string UserName { get; set; }
    }
}
