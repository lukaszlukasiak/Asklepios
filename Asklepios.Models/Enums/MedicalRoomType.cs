using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Asklepios.Core.Enums
{
    //[Flags]
    public enum MedicalRoomType
    {
        [Description("Ogólny")]
        General,
        [Description("Kardiologiczny")]
        Cardiological,
        [Description("Ginekologiczny")]
        Gynecological,
        [Description("Laryngologiczny")]
        Laryngological,
        [Description("Urologiczny")]
        Urological,
        [Description("Chirurgiczny")]
        Surgical,
        [Description("Ortopedyczny")]
        Orthopedic,
        [Description("Gabinet zabiegowy")]
        Treatment,
        [Description("Gastrologiczny")]
        Gastrological,
        [Description("Okulistyczny")]
        Ophthalmology,
        [Description("Neurologiczny")]
        Neurological,
        [Description("Stomatologiczny")]
        Dental,
        [Description("Higieny jamy ustnej")]
        OralHygiene,
        [Description("Rehabilitacyjny")]
        Rehabilitation,
        [Description("Diagnostyki obrazowej")]
        MedicalImaging
    }
}
