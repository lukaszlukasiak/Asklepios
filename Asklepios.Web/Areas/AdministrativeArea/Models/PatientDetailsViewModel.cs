using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Asklepios.Web.Areas.AdministrativeArea.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asklepios.Web.Extensions;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class PatientDetailsViewModel:IPatientSearch
    {
        private Patient _currentPatient;
        public Patient CurrentPatient 
        { 
            get
            {
                return _currentPatient;
            }
            set
            {
                if (value!=null)
                {
                    _currentPatient = value;
                    CurrentPatientId = _currentPatient.Id;
                }
            }
        }
        public long  CurrentPatientId { get; set; }

        public string SelectedName { get; set; }
        public string SelectedSurname { get; set; }
        public string SelectedPESEL { get; set; }
        public string SelectedPassportNumber { get; set; }
        public string SelectedPassportCode { get; set; }

        public Aglomeration? SelectedAglomeration { get; set; }
        private long _selectedMedicalPackageId;

        public long SelectedMedicalPackageId 
        {
            get
            {
                if (_selectedMedicalPackageId<=0)
                {
                    if (SelectedMedicalPackage!=null)
                    {
                        return SelectedMedicalPackage.Id;
                    }
                }
                return _selectedMedicalPackageId;
            }
            set
            {
                _selectedMedicalPackageId = value;
            }
        }
        public MedicalPackage SelectedMedicalPackage { get; set; }
        private long _selectedNFZUnitId;

        public long SelectedNFZUnitId 
        { 
            get
            {
                if (_selectedNFZUnitId<= 0)
                {
                    if (SelectedNFZUnit != null)
                    {
                        return SelectedNFZUnit.Id;
                    }
                }
                return _selectedNFZUnitId;
            }
            set
            {
                _selectedNFZUnitId = value;
            }
        }
        public NFZUnit SelectedNFZUnit { get; set; }
        public bool? HasPolishCitizenship { get; set; }
        public ViewMode? ViewMode { get; set; }
        public long? SelectedId { get ; set ; }
        public Gender? SelectedGender { get ; set ; }

        public List<MedicalPackage> MedicalPackages { get; set; }
        public List<NFZUnit> NFZUnits { get; set; }
        public IFormFile ImageFile { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public bool IsValid
        {
            get
            {
                if (CurrentPatient!=null)
                {
                    return CurrentPatient.User.IsValid && CurrentPatient.Person.IsValid && CurrentPatient.IsValid;
                }
                else
                {
                    return false;
                }
            }
        }


        public void UpdateWithSearch(IPatientSearch search)
        {
            SelectedAglomeration = search.SelectedAglomeration;
            SelectedGender = search.SelectedGender;
            SelectedId = search.SelectedId;
            SelectedMedicalPackageId = search.SelectedMedicalPackageId;
            SelectedName = search.SelectedName;
            SelectedNFZUnitId = search.SelectedNFZUnitId;
            SelectedPassportNumber = search.SelectedPassportNumber;
            SelectedPESEL = search.SelectedPESEL;
            SelectedSurname = search.SelectedSurname;
            SuccessMessage = search.SuccessMessage;
            ErrorMessage = search.ErrorMessage;
        }

        //public SelectList GetEnumSelectListWithDefaultValue<TEnum>(TEnum defaultValue)
        //{
            
        //    //SelectList selectListItems= 
        //    //return new SelectList(MedicalPackages).Where(c=>c.Value==defaultValue).Selected;
        //}



    }

}
