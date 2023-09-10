using Asklepios.Core.Enums;
using Asklepios.Web.Enums;
using Asklepios.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class MedicalWorkerSearchOptions:IBaseViewModel
    {
        [Display(Name = "Id pracownika medycznego")]
        public long? SelectedId { get; set; }

        [Display(Name = "Imię")]

        public string SelectedName { get; set; }
        [Display(Name = "Nazwisko")]

        public string SelectedSurname { get; set; }
        [Display(Name = "PESEL")]

        public string SelectedPESEL { get; set; }
        [Display(Name = "Numer paszportu")]

        public string SelectedPassportNumber { get; set; }
        [Display(Name = "Kod paszportu")]

        public string SelectedPassportCode { get; set; }
        [Display(Name = "Świadczy usługę")]
        public long SelectedServiceId { get; set; }


        [Display(Name = "Aglomeracja")]
        public Aglomeration? SelectedAglomeration { get; set; }
        [Display(Name = "Płeć")]
        public Gender? SelectedGender { get; set; }
        [Display(Name = "Kategoria")]
        public MedicalWorkerType? SelectedWorkerType { get; set; }

        public bool IsFilterOn
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(SelectedName))
                {
                    return true;
                }
                if (!string.IsNullOrWhiteSpace(SelectedSurname))
                {
                    return true;
                }
                if (!string.IsNullOrWhiteSpace(SelectedPESEL))
                {
                    return true;
                }
                if (!string.IsNullOrWhiteSpace(SelectedPassportNumber))
                {
                    return true;
                }
                //if (SelectedMedicalPackageId > 0)
                //{
                //    return true;
                //}
                if (SelectedAglomeration.HasValue == true)
                {
                    return true;
                }
                //if (HasPolishCitizenship.HasValue == true)
                //{
                //    return true;
                //}
                //if (SelectedNFZUnitId > 0)
                //{
                //    return true;
                //}
                if (SelectedId > 0)
                {
                    return true;
                }
                if (SelectedGender.HasValue)
                {
                    return true;
                }
                if (SelectedServiceId>0)
                {
                    return true;
                }
                if (SelectedWorkerType.HasValue)
                {
                    return true;
                }
                return false;
            }
        }

        public string UserName { get; set; }
        //public string Message { get; set; }
        //public AlertMessageType AlertMessageType { get; set; }
        public ViewMessage ViewMessage { get; set; } = new ViewMessage();
    }
}
