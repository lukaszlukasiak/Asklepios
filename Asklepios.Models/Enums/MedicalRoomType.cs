using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Asklepios.Core.Enums
{
    //[Flags]
    public enum MedicalRoomType
    {
        [Display(Name = "Gabinet ogólny")]
        [Description("Gabinet ogólny")]
        General,
        [Display(Name = "Gabinet kardiologiczny")]
        [Description("Gabinet kardiologiczny")]
        Cardiological,
        [Display(Name = "Gabinet ginekologiczny")]
        [Description("Gabinet ginekologiczny")]
        Gynecological,
        [Display(Name = "Gabinet laryngologiczny")]
        [Description("Gabinet laryngologiczny")]
        Laryngological,
        //[Description("Urologiczny")]
        //Urological,
        [Display(Name = "Gabinet chirurgiczny")]
        [Description("Gabinet chirurgiczny")]
        Surgical,
        //[Description("Ortopedyczny")]
        //Orthopedic,
        [Display(Name = "Gabinet zabiegowy")]
        [Description("Gabinet zabiegowy")]
        Treatment,
        //[Description("Gastrologiczny")]
        //Gastrological,
        [Display(Name = "Gabinet okulistyczny")]
        [Description("Gabinet okulistyczny")]
        Ophthalmology,
        [Display(Name = "Gabinet neurologiczny")]
        [Description("Gabinet neurologiczny")]
        Neurological,
        [Display(Name = "Gabinet stomatologiczny")]
        [Description("Gabinet stomatologiczny")]
        Dental,
        [Display(Name = "Gabinet higieny jamy ustnej")]
        [Description("Gabinet higieny jamy ustnej")]
        OralHygiene,
        [Display(Name = "Gabinet rehabilitacyjny")]
        [Description("Gabinet rehabilitacyjny")]
        Rehabilitation,
        [Display(Name = "Gabinet diagnostyki obrazowej")]
        [Description("Gabinet diagnostyki obrazowej")]
        MedicalImaging
    }
}
