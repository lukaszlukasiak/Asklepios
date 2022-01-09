using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public class PrescriptionsViewModel
    {
        public PrescriptionsViewModel(Visit visit, Prescription prescription)
        {
            Visit = visit;
            Prescription = prescription;
        }

        public Visit Visit { get; }
        public Prescription Prescription { get; }
    }
}
