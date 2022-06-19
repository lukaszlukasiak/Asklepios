using Asklepios.Core.Models;
using Asklepios.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public class BookVisitViewModel:SearchViewModel, IBaseViewModel
    {
        public List<Visit> AllVisitsList { get; set; }
        public List<MedicalService> AllMedicalServices { get; set; }
        public List<VisitCategory> AllCategories { get; set; }
        public List<Location> AllLocations { get; set; }
        public List<MedicalWorker> AllMedicalWorkers { get; set; }
        public List<Visit> _filteredVisits;
        public List<Visit> FilteredVisits
        {
            get
            {
                if (_filteredVisits==null)
                {
                    _filteredVisits = FilterVisits();
                }
                
                return _filteredVisits;
            }

        }

        public int CurrentPageNum { get; set; } = 0;
        public int NumberOfPages
        {
            get
            {
                if (FilteredVisits!=null)
                {
                    return FilteredVisits.Count / ItemsPerPage;
                }
                else
                {
                    return 0;
                }
            }
        }
        const int ItemsPerPage = 100;

        public bool NoReferral { get; set; }

        public BookVisitViewModel()
        {
            //SearchOptions = new SearchViewModel();

        }
        //public BookVisitViewModel (List<Visit> visits, VisitSearchOptions searchOptions)
        //{
        //    AllVisitsList = visits;
        //    SearchOptions = searchOptions;
        //    //FilteredVisits = FilterVisits();
        //}

        public List<Location> GetLocations
        {
            get
            {
                if (AllVisitsList?.Count > 0)
                {
                    List<Location> locations = AllVisitsList.Select(c => c.Location).Distinct().ToList();
                    return locations;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<MedicalWorker> GetMedicalWorkers
        {
            get
            {
                if (AllVisitsList?.Count > 0)
                {
                    List<MedicalWorker> workers = AllVisitsList.Select(c => c.MedicalWorker).Distinct().ToList();
                    return workers;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<MedicalService> GetMedicalServices
        {
            get
            {
                if (AllVisitsList?.Count > 0)
                {
                    List<MedicalService> services = AllVisitsList.Select(c => c.PrimaryService).Distinct().ToList();
                    return services;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<VisitCategory> GetVisitCategories
        {
            get
            {
                if (AllVisitsList?.Count > 0)
                {
                    List<VisitCategory> categories = AllVisitsList.Select(c => c.VisitCategory).Distinct().ToList();
                    return categories;
                }
                else
                {
                    return null;
                }
            }
        }

        public string UserName { get; set; }
        public List<Notification> Notifications { get; set; }

        private List<Visit> FilterVisits()
        {
            //if (SearchOptions!=null)
            //{
            //    return AllVisitsList;
            //}
            List<Visit> filteredVisits = AllVisitsList;
            if (AllVisitsList == null)
            {
                return null;
            }
            else
            {
                if (SelectedMedicalWorkerId != null)
                {
                    if (long.TryParse(SelectedMedicalWorkerId, out long lid))
                    {
                        if (lid>0)
                        {
                            filteredVisits = filteredVisits.Where(c => c.MedicalWorker.Id == lid).ToList();
                            if (filteredVisits == null)
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            if (SelectedLocationId != null)
            {
                if (long.TryParse(SelectedLocationId, out long lid))
                {
                    if (lid>0)
                    {
                        filteredVisits = filteredVisits.Where(c => c.Location.Id == lid).ToList();
                        if (filteredVisits == null)
                        {
                            return null;
                        }
                    }
                   
                }
            }
            if (SelectedServiceId != null)
            {
                if (long.TryParse(SelectedServiceId, out long lid))
                {
                    if (lid>0)
                    {
                        filteredVisits = filteredVisits.Where(c => c.PrimaryService.Id == lid).ToList();
                        if (filteredVisits == null)
                        {
                            return null;
                        }
                    }
                    
                }
            }
            if (SelectedCategoryId != null)
            {
                if (long.TryParse(SelectedCategoryId, out long lid))
                {
                    if (lid>0)
                    {
                        filteredVisits = filteredVisits.Where(c => c.VisitCategory.Id == lid).ToList();
                        if (filteredVisits == null)
                        {
                            return null;
                        }
                    }                    
                }
            }
            //if (SelectedVoivodeship!=null)
            //{
            //    filteredVisits = AllVisitsList.Where(c => c.Location.VoivodeshipType == SearchOptions.SelectedVoivodeship).ToList();
            //    if (filteredVisits == null)
            //    {
            //        return null;
            //    }
            //}
            filteredVisits = filteredVisits.OrderBy(c => c.DateTimeSince).ToList();
            if (filteredVisits.Count<ItemsPerPage)
            {
                return filteredVisits;
            }
            else
            {
                return filteredVisits.GetRange(CurrentPageNum, ItemsPerPage);
            }           
        }
    }
}

