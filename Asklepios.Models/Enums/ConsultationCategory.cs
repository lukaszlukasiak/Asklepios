using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Asklepios.Models.Enums
{
    public enum ConsultationCategory
    {
        [Description("Interna")]
        Internist,
        [Description("Pediatria")]
        Pediatrician,
        [Description("Kardiologia")]
        Cardiological,
        [Description("Ginekologia")]
        Gynecological,
        [Description("Dermatologia")]
        Dermatological,
        [Description("Laryngologia")]
        Laryngological,
        [Description("Ortopedia")]
        Orthopedist,
        [Description("Urologia")]
        Urological,
        [Description("Chirurgia")]
        Surgical,
        [Description("Pokój zabiegowy")]
        Treatment,
        [Description("Gastrologia")]
        Gastrological,
        [Description("Okulistyka")]
        Ophthalmology,
        [Description("Neurologia")]
        Neurological,
        [Description("Stomatologia")]
        Dental,
        [Description("Higiena jamy ustnej")]
        OralHygiene,
        [Description("Rehabilitacja")]
        Rehabilitation,
        [Description("Diagnostyka obrazowa")]
        MedicalImaging,
        [Description("Onkologia")]
        Oncology,
        [Description("Endokrynolog")]
        Endocrinologist
    }
}
