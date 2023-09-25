using Asklepios.Core.Models;
using Asklepios.Web.Enums;
using Asklepios.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class MedicalWorkersManageViewModel:IBaseViewModel
    {
        public MedicalWorker SelectedWorker { get; set; }
        public MedicalWorkerSearchOptions SearchOptions { get; set; } = new MedicalWorkerSearchOptions();
        public long SelectedWorkerId { get; set; }
        public MedicalWorkertData MedicalWorkertData { get; set; }
        public User User { get; set; }
        public Person Person { get; set; }

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

        public List<MedicalService> PrimaryServices { get; set; }
        public int ItemsPerPage { get; private set; } = 100;
        public int CurrentPageNum { get; private set; } = 1;
        public ViewMode ViewMode { get; set; }
        public string UserName { get; set; }
        public ViewMessage ViewMessage { get; set; } = new ViewMessage();

        private List<MedicalWorker> GetFilteredWorkersList()
        {
            List<MedicalWorker> filteredWorkers = AllMedicalWorkers;
            if (AllMedicalWorkers == null)
            {
                return null;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(SearchOptions.SelectedName))
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
            if (SearchOptions.SelectedServiceId > 0)
            {
                foreach (MedicalWorker www in AllMedicalWorkers)
                {
                    www.MedicalServiceIds = www.MedicalServices.Select(c => c.Id).ToList();
                }

                filteredWorkers = filteredWorkers.Where(c => c.MedicalServiceIds.Contains(SearchOptions.SelectedServiceId)).ToList();
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
            if (SearchOptions.SelectedWorkerType.HasValue)
            {
                filteredWorkers = filteredWorkers.Where(c => c.MedicalWorkerType == SearchOptions.SelectedWorkerType).ToList();
                if (filteredWorkers == null)
                {
                    return null;
                }
            }
            if (SearchOptions.SelectedAglomeration.HasValue)
            {
                filteredWorkers = filteredWorkers.Where(c => c.Person.DefaultAglomeration == SearchOptions.SelectedAglomeration).ToList();
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
