using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public class SelectPatientViewModel:IBaseViewModel
    {
        public IQueryable<Patient> AllPatients { get; set; }
        public string SelectedPESEL { get; set; }
        public string SelectedPassportNumber { get; set; }
        public string SelectedName { get; set; }
        public string SelectedSurname { get; set; }
        public string SelectedId { get; set; }
        private List<Patient> _filteredPatients;
        public List<Patient> FilteredPatients
        {
            get
            {
                if (_filteredPatients == null)
                {
                    _filteredPatients = FilterPatients();
                }

                return _filteredPatients;
            }

        }

        public Patient SelectedPatient { get; set; }
        public string UserName { get;set; }

        private List<Patient> FilterPatients()
        {
            IQueryable <Patient> filteredPatients = AllPatients;
             if (AllPatients==null)
           {
                return null;
            }
            if (!String.IsNullOrWhiteSpace(SelectedId))
            {
                if (long.TryParse(SelectedId,out long lId))
                {
                    filteredPatients = filteredPatients
                        .Where(c => c.Id == lId)
                        .AsQueryable();
                    if (filteredPatients == null)
                    {
                        return null;
                    }

                }
            }

            if (!String.IsNullOrWhiteSpace(SelectedName))
            {
                filteredPatients = filteredPatients
                    .Where(c => c.Person.Name.Contains(SelectedName))
                    .AsQueryable();
                if (filteredPatients == null)
                {
                    return null;
                }
            }
            if (!String.IsNullOrWhiteSpace(SelectedSurname))
            {
                filteredPatients = filteredPatients
                    .Where(c => c.Person.Surname.Contains(SelectedSurname))
                    .AsQueryable();
                if (filteredPatients == null)
                {
                    return null;
                }
            }
            if (!String.IsNullOrWhiteSpace(SelectedPESEL))
            {
                filteredPatients = filteredPatients
                    .Where(c => c.Person.PESEL.Contains(SelectedPESEL))
                    .AsQueryable();
                if (filteredPatients == null)
                {
                    return null;
                }
            }
            if (!String.IsNullOrWhiteSpace(SelectedPassportNumber))
            {
                filteredPatients = filteredPatients
                    .Where(c => c.Person.PassportNumber.Contains(SelectedPassportNumber))
                    .AsQueryable();
            }
            return filteredPatients.ToList();
        }
        public SelectPatientViewModel(Patient selectedPatient, IQueryable<Patient> patients)
        {
            SelectedPatient = selectedPatient;
            AllPatients = patients;
        }
        public SelectPatientViewModel()
        {
        }


    }
}
