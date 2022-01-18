using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Asklepios.Core.Enums
{
    //[Flags]
    public enum MedicalRoomType
    {
        [Description("Gabinet ogólny")]
        General,
        [Description("Gabinet kardiologiczny")]
        Cardiological,
        [Description("Gabinet ginekologiczny")]
        Gynecological,
        [Description("Gabinet laryngologiczny")]
        Laryngological,
        //[Description("Urologiczny")]
        //Urological,
        [Description("Gabinet chirurgiczny")]
        Surgical,
        //[Description("Ortopedyczny")]
        //Orthopedic,
        [Description("Gabinet zabiegowy")]
        Treatment,
        //[Description("Gastrologiczny")]
        //Gastrological,
        [Description("Gabinet okulistyczny")]
        Ophthalmology,
        [Description("Gabinet neurologiczny")]
        Neurological,
        [Description("Gabinet stomatologiczny")]
        Dental,
        [Description("Gabinet higieny jamy ustnej")]
        OralHygiene,
        [Description("Gabinet rehabilitacyjny")]
        Rehabilitation,
        [Description("Gabinet diagnostyki obrazowej")]
        MedicalImaging
    }
}
