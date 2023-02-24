using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Enums
{
    public enum IdentityRoleTypes
    {
        [Description("CustomerService")]
        CustomerService,
        [Description("Patient")]
        Patient,
        [Description("AdministrativeWorker")]
        AdministrativeWorker,
        [Description("MedicalWorker")]
        MedicalWorker
    }

}
