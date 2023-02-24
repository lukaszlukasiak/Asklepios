using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Interfaces
{
    public interface IPatientSearch
    {
        [Display(Name = "Id pacjenta")]
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

        [Display(Name = "Aglomeracja")]
        public Aglomeration? SelectedAglomeration { get; set; }
        [Display(Name = "Pakiet medyczny")]
        public long SelectedMedicalPackageId { get; set; }
        public MedicalPackage SelectedMedicalPackage { get; set; }
        [Display(Name = "Oddział NFZ")]
        public long SelectedNFZUnitId { get; set; }
        public NFZUnit SelectedNFZUnit { get; set; }
        [Display(Name = "Polskie obywatelstwo")]
        public bool? HasPolishCitizenship { get; set; }
        [Display(Name = "Płeć")]
        public Gender? SelectedGender { get; set; }
        //public string SuccessMessage { get; set; }
        //public string ErrorMessage { get; set; }

        public void SetSearchOptions(IPatientSearch iSearch)
        {
            IPatientSearch thisSearch = this;
            thisSearch.SelectedAglomeration = iSearch.SelectedAglomeration;
            thisSearch.SelectedMedicalPackageId = iSearch.SelectedMedicalPackageId;
            thisSearch.SelectedName= iSearch.SelectedName;
            thisSearch.SelectedNFZUnitId = iSearch.SelectedNFZUnitId;
            thisSearch.SelectedPassportNumber= iSearch.SelectedPassportNumber;
            thisSearch.SelectedPassportCode = iSearch.SelectedPassportCode;
            thisSearch.SelectedPESEL= iSearch.SelectedPESEL;
            thisSearch.SelectedSurname= iSearch.SelectedSurname;
            thisSearch.SelectedGender = iSearch.SelectedGender;
            thisSearch.HasPolishCitizenship = iSearch.HasPolishCitizenship;
            //thisSearch.ErrorMessage = iSearch.ErrorMessage;
            //thisSearch.SuccessMessage = iSearch.SuccessMessage;
        }

    }
}
