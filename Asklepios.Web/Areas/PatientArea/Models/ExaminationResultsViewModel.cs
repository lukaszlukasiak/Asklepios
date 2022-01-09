using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public class ExaminationResultsViewModel
    {
        public ExaminationResultsViewModel(MedicalTestResult medicalTestResult, Visit visit)
        {
            Visit = visit;
            MedicalTestResult = medicalTestResult;
        }

        public Visit Visit { get; }
        public MedicalTestResult MedicalTestResult { get; }
    }
}
