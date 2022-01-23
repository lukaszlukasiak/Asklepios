using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class PatientAddViewModel
    {
        public User User { get; set; }
        public Person Person { get; set; }
        public Patient Patient { get; set; }


        public List<MedicalPackage> MedicalPackages { get; set; }


    }
}
