using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Enums
{
    public enum AlertMessageType
    {
        [Description("InfoMessage")]
        InfoMessage,
        [Description("WarningMessage")]
        WarningMessage,
        [Description("ErrorMessage")]
        ErrorMessage,
        [Description("SuccessMessage")]
        SuccessMessage
    }
}
