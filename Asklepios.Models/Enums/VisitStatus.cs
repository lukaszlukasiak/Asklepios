using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Enums
{
    public enum VisitStatus
    {
        AvailableNotBooked,
        Booked,
        Active,
        Finished,
        NotHeldAbsentPatient,
        NotHeldOther,
        Cancelled
    }
}
