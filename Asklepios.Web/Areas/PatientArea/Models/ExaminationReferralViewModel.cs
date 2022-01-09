using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public class ExaminationReferralViewModel
    {
        public ExaminationReferralViewModel(MedicalReferral referral, Visit visit)
        {
            ExaminationReferral = referral;
            Visit = visit;
        }

        public MedicalReferral ExaminationReferral { get; }
        public Visit Visit { get; }
    }
}
