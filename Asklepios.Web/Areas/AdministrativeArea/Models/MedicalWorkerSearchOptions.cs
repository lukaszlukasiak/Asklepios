﻿using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class MedicalWorkerSearchOptions
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
        [Display(Name = "Płeć")]
        public Gender? SelectedGender { get; set; }

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
                return false;
            }
        }
    }
}
