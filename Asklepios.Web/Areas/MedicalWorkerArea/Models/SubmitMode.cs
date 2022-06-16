using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public enum SubmitMode
    {
        AddReferral,
        AddPrescription,
        AddTestRestult,
        AddMedicineToPrescription,
        Other
    }
}
