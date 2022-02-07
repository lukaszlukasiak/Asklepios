using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Asklepios.Core.Enums
{
    public enum MedicalWorkerType
    {
        [Display(Name = "Lekarz")]
        [Description("Lekarz")]
        Doctor,
        [Display(Name = "Pielęgniarka")]
        [Description("Pielęgniarka")]
        Nurse,
        [Display(Name = "Fizjoterapeuta")]
        [Description("Fizjoterapeuta")]
        Physiotherapist,
        [Display(Name = "Technik elektroradiolog")]
        [Description("Technik elektroradiolog")]
        ElectroriadologyTechnician,
        [Display(Name = "Higienistka dentystyczna")]
        [Description("Higienistka dentystyczna")]
        DentalHygienist
    }
}
