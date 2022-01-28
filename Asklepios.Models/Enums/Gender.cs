using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Enums
{
    public enum Gender
    {
        [Display(Name = "Mężczyzna")]
        [Description("Mężczyzna")]
        Male,
        [Display(Name = "Kobieta")]
        [Description("Kobieta")]
        Female
    }
}
