using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Asklepios.Web.Areas.AdministrativeArea.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class PatientDetailsViewModel:ISearchPatient
    {
        public Patient CurrentPatient { get; set; }
        public string SelectedName { get; set; }
        public string SelectedSurname { get; set; }
        public string SelectedPESEL { get; set; }
        public string SelectedPassportNumber { get; set; }
        public Aglomeration? SelectedAglomeration { get; set; }
        public long SelectedMedicalPackageId { get; set; }
        public MedicalPackage SelectedMedicalPackage { get; set; }
        public long SelectedNFZUnitId { get; set; }
        public NFZUnit SelectedNFZUnit { get; set; }
        public bool? HasPolishCitizenship { get; set; }

        public void UpdateWithSearch(ISearchPatient search)
        {
            //SelectedAglomeration=search.sel
        }

    }
}
