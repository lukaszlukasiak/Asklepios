using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public class SelectPatientViewModel:IBaseViewModel
    {
        public List<Patient> AllPatients { get; set; }
        public string SelectedPESEL { get; set; }
        public string SelectedPassportNumber { get; set; }
        public string SelectedName { get; set; }
        public string SelectedSurname { get; set; }
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
            List<Patient> filteredPatients = AllPatients;
            if (AllPatients==null)
            {
                return null;
            }

            if (!String.IsNullOrWhiteSpace(SelectedName))
            {
                filteredPatients = filteredPatients.Where(c => c.Person.Name == SelectedName).ToList();
                if (filteredPatients == null)
                {
                    return null;
                }
            }
            if (!String.IsNullOrWhiteSpace(SelectedSurname))
            {
                filteredPatients = filteredPatients.Where(c => c.Person.Surname == SelectedSurname).ToList();
                if (filteredPatients == null)
                {
                    return null;
                }
            }
            if (!String.IsNullOrWhiteSpace(SelectedPESEL))
            {
                filteredPatients = filteredPatients.Where(c => c.Person.PESEL == SelectedPESEL).ToList();
                if (filteredPatients == null)
                {
                    return null;
                }
            }
            if (!String.IsNullOrWhiteSpace(SelectedPassportNumber))
            {
                filteredPatients = filteredPatients.Where(c => c.Person.PassportNumber == SelectedPassportNumber).ToList();
            }
            return filteredPatients;
        }
        public SelectPatientViewModel(Patient selectedPatient, List<Patient> patients)
        {
            SelectedPatient = selectedPatient;
            AllPatients = patients;
        }
        public SelectPatientViewModel()
        {
        }


    }
}
