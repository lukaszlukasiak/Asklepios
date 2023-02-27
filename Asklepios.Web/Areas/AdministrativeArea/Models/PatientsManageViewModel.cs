using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Asklepios.Web.Areas.AdministrativeArea.Interfaces;
using Asklepios.Web.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class PatientsManageViewModel:IPatientSearch,IBaseViewModel
    {
        public Person Person { get; set; } = new Person();
        public Patient Patient { get; set; }
        public List<MedicalPackage> MedicalPackages { get; set; }
        public List<NFZUnit> NFZUnits { get; set; }
        public IQueryable<Patient> AllPatients { get; set; }
        public List<Patient> FilteredPatients
        {
            get
            {
                if (IsFilterOn)
                {
                    return GetFilteredPatientsList();
                }
                else
                {
                    return AllPatients?.OrderBy(c => c.Person.Surname).ThenBy(d=>d.Person.Name).ToList(); ;
                }
            }
        }
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
                if (SelectedMedicalPackageId > 0)
                {
                    return true;
                }
                if (SelectedAglomeration.HasValue == true)
                {
                    return true;
                }
                if (HasPolishCitizenship.HasValue == true)
                {
                    return true;
                }
                if (SelectedNFZUnitId>0)
                {
                    return true;
                }
                if (SelectedId>0)
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
        public int ItemsPerPage { get; private set; } = 100;
        public int CurrentPageNum { get; private set; } = 1;
        //public string SuccessMessage { get; set; }
        //public string ErrorMessage { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public AlertMessageType AlertMessageType { get; set; }

        private List<Patient> GetFilteredPatientsList()
        {
            IQueryable<Patient> filteredPatients = AllPatients;
            if (AllPatients== null)
            {
                return null;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(SelectedName))
                {
                    filteredPatients = filteredPatients.Where(c => c.Person.Name.Contains(SelectedName,StringComparison.OrdinalIgnoreCase)).AsQueryable();
                    if (filteredPatients == null)
                    {
                        return null;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(SelectedSurname))
            {
                filteredPatients = filteredPatients.Where(c => c.Person.Surname.Contains(SelectedSurname, StringComparison.OrdinalIgnoreCase)).AsQueryable();
                if (filteredPatients == null)
                {
                    return null;
                }
            }
            if (!string.IsNullOrWhiteSpace(SelectedPESEL))
            {
                filteredPatients = filteredPatients.Where(c => c.Person.PESEL.Contains(SelectedPESEL)).AsQueryable();
                if (filteredPatients == null)
                {
                    return null;
                }
            }
            if (!string.IsNullOrWhiteSpace(SelectedPassportNumber))
            {
                filteredPatients = filteredPatients.Where(c => c.Person.Surname.Contains(SelectedPassportNumber, StringComparison.OrdinalIgnoreCase)).AsQueryable();
                if (filteredPatients == null)
                {
                    return null;
                }
            }
            if (SelectedMedicalPackageId >0)
            {
                //if (long.TryParse(SelectedVisitCategoryId, out long lid))
                //{
                //    if (lid > 0)
                //    {
                filteredPatients = filteredPatients.Where(c => c.MedicalPackage.Id == SelectedMedicalPackageId).AsQueryable();
                if (filteredPatients == null)
                        {
                            return null;
                        }
                //    }
                //}
            }
            if (SelectedNFZUnitId> 0)
            {
                //if (long.TryParse(SelectedVisitCategoryId, out long lid))
                //{
                //    if (lid > 0)
                //    {
                filteredPatients = filteredPatients.Where(c => c.NFZUnit.Id == SelectedNFZUnitId).AsQueryable();
                if (filteredPatients == null)
                {
                    return null;
                }
                //    }
                //}
            }
            if (SelectedId> 0)
            {
                filteredPatients = filteredPatients.Where(c => c.Id == SelectedId).AsQueryable();
                if (filteredPatients == null)
                {
                    return null;
                }
            }
            if (SelectedGender.HasValue)
            {
                filteredPatients = filteredPatients.Where(c => c.Person.Gender== SelectedGender).AsQueryable();
                if (filteredPatients == null)
                {
                    return null;
                }
            }
            if (SelectedAglomeration.HasValue)
            {
                filteredPatients = filteredPatients.Where(c => c.Person.DefaultAglomeration == SelectedAglomeration).AsQueryable();
                if (filteredPatients == null)
                {
                    return null;
                }
            }

            //if (VisitsDateFrom.HasValue)
            //{
            //    filteredPatients = filteredPatients.Where(c => c.DateTimeSince >= VisitsDateFrom).ToList();
            //    if (filteredPatients == null)
            //    {
            //        return null;
            //    }

            //}
            //if (VisitsDateTo.HasValue)
            //{
            //    filteredPatients = filteredPatients.Where(c => c.DateTimeSince <= VisitsDateTo.Value.AddDays(1)).ToList();
            //    if (filteredPatients == null)
            //    {
            //        return null;
            //    }

            //}



            filteredPatients = filteredPatients.OrderBy(c => c.Person.Surname).ThenBy(d=>d.Person.Name).AsQueryable();
            if (filteredPatients.Count() < ItemsPerPage)
            {
                return filteredPatients.ToList();
            }
            else
            {
                return filteredPatients.Skip((CurrentPageNum - 1) * ItemsPerPage).Take( ItemsPerPage).ToList();
            }
        }
    }
}
