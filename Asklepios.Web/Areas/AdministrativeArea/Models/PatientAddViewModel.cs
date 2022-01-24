using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class PatientAddViewModel
    {
        public User User { get; set; } = new User();
        public Person Person { get; set; } = new Person();
        public Patient Patient { get; set; }


        public List<MedicalPackage> MedicalPackages { get; set; }
        public List<NFZUnit> NFZUnits { get; set; }
        public IFormFile ImageFile { get; set; }

        public bool IsValid
        {
            get
            {
                return User.IsValid && Person.IsValid && Patient.IsValid;
            }
        }

    }
}
