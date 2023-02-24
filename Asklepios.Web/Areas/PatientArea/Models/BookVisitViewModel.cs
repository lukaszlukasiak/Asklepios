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
        public IQueryable<Visit> PreFilteredVisitsList { get; set; }

        public List<MedicalService> AllMedicalServices { get; set; }
        public List<VisitCategory> AllCategories { get; set; }
        public List<Location> AllLocations { get; set; }
        public List<MedicalWorker> AllMedicalWorkers { get; set; }
        private List<Visit> _filteredVisits;
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

        public int CurrentPageNum { get; set; } = 1;
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
        public bool HasAnyFilterSelected
        {
            get
            {
                return ((!string.IsNullOrWhiteSpace(SelectedCategoryId)) || (!string.IsNullOrWhiteSpace(SelectedLocationId)) || (!string.IsNullOrWhiteSpace(SelectedMedicalWorkerId)) || (!string.IsNullOrWhiteSpace(SelectedServiceId)));
            }
        }

        public BookVisitViewModel(SearchViewModel model)
        {
            SelectedCategoryId = model.SelectedCategoryId;
            SelectedLocationId = model.SelectedLocationId;
            SelectedMedicalWorkerId = model.SelectedMedicalWorkerId;
            SelectedServiceId = model.SelectedServiceId;

            HasPredefinedService = model.HasPredefinedService;
            HasPredefinedMedicalWorker = model.HasPredefinedMedicalWorker;
            HasPredefinedLocation = model.HasPredefinedLocation;
            HasPredefinedCategory = model.HasPredefinedCategory;
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
                if (PreFilteredVisitsList==null || PreFilteredVisitsList.Count()==0)
                {
                    return new List<Location>();
                }
                if (HasAnythingPredefined)
                {
                    if (HasPredefinedLocation)
                    {
                        return AllLocations.Where(c => c.Id == long.Parse(SelectedLocationId)).ToList();
                    }
                    else
                    {
                        return FilteredVisits.Select(c=>c.Location).Distinct().ToList();
                    }
                }
                else
                {
                    if (HasAnyFilterSelected && string.IsNullOrWhiteSpace(SelectedLocationId))
                    {
                        List<Location> locations = PreFilteredVisitsList.Select(c => c.Location).Distinct().ToList();
                        return locations;
                    }
                    else
                    {
                        List<Location> locations = FilteredVisits.Select(c => c.Location).Distinct().ToList();
                        return locations;
                    }
                }
            }
        }
        public List<MedicalWorker> GetMedicalWorkers
        {
            get
            {
                if (PreFilteredVisitsList == null || PreFilteredVisitsList.Count()==0)
                {
                    return new List<MedicalWorker>();
                }
                if (HasAnythingPredefined)
                {
                    if (HasPredefinedMedicalWorker)
                    {
                        List<MedicalWorker> workers= AllMedicalWorkers.Where(c => c.Id == long.Parse(SelectedMedicalWorkerId)).ToList();
                        return workers;
                    }
                    else
                    {
                        return PreFilteredVisitsList.Select(c => c.MedicalWorker).Distinct().ToList();
                    }
                }
                else
                {
                    if (HasAnyFilterSelected &&  string.IsNullOrWhiteSpace(SelectedMedicalWorkerId))
                    {
                        List<MedicalWorker> workers = PreFilteredVisitsList.Select(c => c.MedicalWorker).Distinct().ToList();
                        return workers;
                    }
                    else
                    {
                        List<MedicalWorker> workers = FilteredVisits.Select(c => c.MedicalWorker).Distinct().ToList();
                        return workers;
                    }
                }
            }
        }
        public List<MedicalService> GetMedicalServices
        {
            get
            {
                if (PreFilteredVisitsList == null || PreFilteredVisitsList.Count()==0)
                {
                    return new List<MedicalService>();
                }
                if (HasAnythingPredefined)
                {
                    if (HasPredefinedService)
                    {
                        List<MedicalService> list= AllMedicalServices.Where(c => c.Id == long.Parse(SelectedServiceId)).ToList();
                        return list;
                    }
                    else
                    {
                        return PreFilteredVisitsList.Select(c => c.PrimaryService).Distinct().ToList();
                    }
                }
                else
                {
                    if (HasAnyFilterSelected && string.IsNullOrWhiteSpace(SelectedServiceId))
                    {
                        List<MedicalService> services = PreFilteredVisitsList.Select(c => c.PrimaryService).Distinct().ToList();
                        return services;
                    }
                    else
                    {
                        List<MedicalService> services = FilteredVisits.Select(c => c.PrimaryService).Distinct().ToList();
                        return services;
                    }
                }
            }
        }
        public List<VisitCategory> GetVisitCategories
        {
            get
            {
                if (PreFilteredVisitsList == null || PreFilteredVisitsList.Count()==0)
                {
                    return new List<VisitCategory>();
                }
                if (HasAnythingPredefined)
                {
                    if (HasPredefinedCategory)
                    {
                        return AllCategories.Where(c => c.Id == long.Parse(SelectedCategoryId)).ToList();
                    }
                    else
                    {
                        return PreFilteredVisitsList.Select(c => c.VisitCategory).Distinct().ToList();
                    }
                }
                else
                {
                    if (HasAnyFilterSelected && string.IsNullOrWhiteSpace(SelectedCategoryId))
                    {
                        List<VisitCategory> categories = PreFilteredVisitsList.Select(c => c.VisitCategory).Distinct().ToList();
                        return categories;
                    }
                    else
                    {
                        List<VisitCategory> categories = FilteredVisits.Select(c => c.VisitCategory).Distinct().ToList();
                        return categories;
                    }
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
            IQueryable<Visit> filteredVisits = PreFilteredVisitsList;
            if (PreFilteredVisitsList == null)
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
                            filteredVisits = filteredVisits.Where(c => c.MedicalWorker.Id == lid).AsQueryable();
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
                        filteredVisits = filteredVisits.Where(c => c.Location.Id == lid).AsQueryable();
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
                        filteredVisits = filteredVisits.Where(c => c.PrimaryService.Id == lid).AsQueryable();
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
                        filteredVisits = filteredVisits.Where(c => c.VisitCategory.Id == lid).AsQueryable();
                        if (filteredVisits == null)
                        {
                            return null;
                        }
                    }                    
                }
            }
            filteredVisits = filteredVisits.OrderBy(c => c.DateTimeSince).AsQueryable();
            if (filteredVisits.Count()<ItemsPerPage)
            {
                return filteredVisits                    
                    .ToList();
            }
            else
            {
                return filteredVisits
                    .Skip((CurrentPageNum-1)*ItemsPerPage)
                    .Take(ItemsPerPage).ToList();
            }           
        }
    }
}

