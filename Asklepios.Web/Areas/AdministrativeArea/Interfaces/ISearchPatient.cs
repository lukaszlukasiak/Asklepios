using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Interfaces
{
    public interface ISearchPatient
    {
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

        public void SetSearchOptions(ISearchPatient iSearch)
        {
            ISearchPatient thisSearch = this;
            thisSearch.SelectedAglomeration = iSearch.SelectedAglomeration;
            thisSearch.SelectedMedicalPackageId = iSearch.SelectedMedicalPackageId;
            thisSearch.SelectedName= iSearch.SelectedName;
            thisSearch.SelectedNFZUnitId = iSearch.SelectedNFZUnitId;
            thisSearch.SelectedPassportNumber= iSearch.SelectedPassportNumber;
            thisSearch.SelectedPESEL= iSearch.SelectedPESEL;
            thisSearch.SelectedSurname= iSearch.SelectedSurname;
        }

    }
}
