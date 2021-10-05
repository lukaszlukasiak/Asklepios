﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Asklepios.Core.Enums
{
    public enum WorkerModuleType
    {
        [Description("Obsługa klienta")]
        //[DisplayName(Name = "Obsługa klienta")]
        //[EnumDisplayName(DisplayName = "Super Admin")]
        [Display(Name ="Obsługa klienta")]
        CustomerServiceModule,
        [Description("Administracja")]
        [Display(Name = "Administracja")]
        AdministrativeWorkerModule,
        [Description("Pracownik medyczny")]
        [Display(Name = "Pracownik medyczny")]
        MedicalWorkerModule
    }
}
