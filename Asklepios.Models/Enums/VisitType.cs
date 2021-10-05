using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Asklepios.Models.Enums
{
    public enum VisitType
    {
        [Description("W oddziale")]
        OnSite,
        [Description("Zdalna")]
        Remote
    }
}
