using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class MedicalWorkersManageViewModel
    {
        public MedicalWorkerSearchOptions SearchOptions {get;set;}
        public MedicalWorker Worker { get; set; }
        public List<MedicalWorker> AllMedicalWorkers { get; set; }
        public List<MedicalWorker> FilteredWorkers
        {
            get
            {
                if (SearchOptions.IsFilterOn)
                {
                    return GetFilteredWorkersList();
                }
                else
                {
                    return AllMedicalWorkers?.OrderBy(c => c.Person.FullName).ToList(); ;
                }
            }
        }



        public int ItemsPerPage { get; private set; } = 100;
        public int CurrentPageNum { get; private set; } = 1;
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        private List<MedicalWorker> GetFilteredWorkersList()
        {
            List<MedicalWorker> filteredWorkers = AllMedicalWorkers;
            if (AllMedicalWorkers == null)
            {
                return null;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace( SearchOptions.SelectedName))
                {
                    filteredWorkers = filteredWorkers.Where(c => c.Person.Name.Contains(SearchOptions.SelectedName)).ToList();
                    if (filteredWorkers == null)
                    {
                        return null;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(SearchOptions.SelectedSurname))
            {
                filteredWorkers = filteredWorkers.Where(c => c.Person.Surname.Contains(SearchOptions.SelectedSurname)).ToList();
                if (filteredWorkers == null)
                {
                    return null;
                }
            }
            if (!string.IsNullOrWhiteSpace(SearchOptions.SelectedPESEL))
            {
                filteredWorkers = filteredWorkers.Where(c => c.Person.PESEL.Contains(SearchOptions.SelectedPESEL)).ToList();
                if (filteredWorkers == null)
                {
                    return null;
                }
            }
            if (!string.IsNullOrWhiteSpace(SearchOptions.SelectedPassportNumber))
            {
                filteredWorkers = filteredWorkers.Where(c => c.Person.Surname.Contains(SearchOptions.SelectedPassportNumber)).ToList();
                if (filteredWorkers == null)
                {
                    return null;
                }
            }
            if (SearchOptions.SelectedId > 0)
            {
                filteredWorkers = filteredWorkers.Where(c => c.Id == SearchOptions.SelectedId).ToList();
                if (filteredWorkers == null)
                {
                    return null;
                }
            }
            if (SearchOptions.SelectedGender.HasValue)
            {
                filteredWorkers = filteredWorkers.Where(c => c.Person.Gender == SearchOptions.SelectedGender).ToList();
                if (filteredWorkers == null)
                {
                    return null;
                }
            }


            filteredWorkers = filteredWorkers.OrderBy(c => c.Person.FullName).ToList();
            if (filteredWorkers.Count < ItemsPerPage)
            {
                return filteredWorkers;
            }
            else
            {
                return filteredWorkers.GetRange((CurrentPageNum - 1) * ItemsPerPage, ItemsPerPage);
            }
        }

    }
}
