using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class MedicalWorkerViewModel: IBaseViewModel
    {
        public MedicalWorker MedicalWorker { get; set; }
        public string UserName { get; set; }
    }
}
