using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class MedicalWorkerAddViewModel
    {
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public MedicalWorker MedicalWorker { get; set; }
        public User User { get; set; }
        public Person Person { get; set; }
        //public Aglomeration SelectedAglomeration { get; set; }
        //public List<string> EducationHistory { get; set; }
        //public string Experience { get; set; }
        //public DateTime HiredSince { get; set; }
        //public bool IsCurrentlyHired { get; set; }
        public MedicalWorkerType MedicalWorkerType { get; set; }
        public List<MedicalService> PrimaryServices         { get; set; }

        public bool IsValid
        {
            get
            {
                if (MedicalWorker.IsValid)
                {
                    if (Person.IsValid)
                    {
                        if (User.IsValid)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}
