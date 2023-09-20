using Asklepios.Core.Models;
using Asklepios.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public class BookVisitViewModel: SearchViewModel,IBaseViewModel
    {
        public IQueryable<Visit> AllVisitsList { get; set; }
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
        public int CurrentPageNum { get; set; } = 1;
        public int NumberOfPages
        {
            get
            {
                List<Visit> visits = FilteredVisits;
                if (visits == null)
                {
                    return 0;
                }
                else
                {
                    return FilteredVisits.Count / ItemsPerPage;
                }              
            }
        }
        const int ItemsPerPage = 100;

        public bool NoReferral { get; set; }

        public ViewMessage ViewMessage { get; set; }

        public BookVisitViewModel(Patient patient)
        {
            SelectedPatient = patient;
        }
        public BookVisitViewModel()
        {
        }

        public List<Location> GetLocations
        {
            get
            {
                if (FilteredVisits == null)
                {
                    return null;
                }
                if (IsFilterActive)
                {
                    if (FilteredVisits.Count() > 0)
                    {
                        List<Location> locations = FilteredVisits
                            .Select(c => c.Location)
                            .Distinct()
                            .OrderBy(d => d.Name)
                            .ToList();
                        return locations;
                    }
                    else
                    {
                        return new List<Location>();
                    }
                }
                else
                {
                    return AllLocations;
                }
            }
        }
        public List<MedicalWorker> GetMedicalWorkers
        {
            get
            {
                if (IsFilterActive)
                {
                    if (FilteredVisits == null)
                    {
                        return null;
                    }
                    if (FilteredVisits.Count() > 0)
                    {
                        List<MedicalWorker> workers = FilteredVisits
                            .Select(c => c.MedicalWorker)
                            .Distinct()
                            .OrderBy(d => d.FullProffesionalName)
                            .ToList();
                        return workers;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return AllMedicalWorkers;
                }
            }
        }
        public List<MedicalService> GetMedicalServices
        {
            get
            {
                if (IsFilterActive)
                {
                    if (FilteredVisits == null)
                    {
                        return null;
                    }
                    if (FilteredVisits.Count() > 0)
                    {
                        List<MedicalService> services = FilteredVisits
                            .Select(c => c.PrimaryService)
                            .Distinct()
                            .OrderBy(d=>d.Name)
                            .ToList();
                        return services;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return AllMedicalServices;
                }
            }
        }
        public List<VisitCategory> GetVisitCategories
        {
            get
            {
                if (IsFilterActive)
                {
                    if (FilteredVisits == null)
                    {
                        return null;
                    }
                    if (FilteredVisits.Count() > 0)
                    {
                        List<VisitCategory> categories = FilteredVisits
                            .Select(c => c.VisitCategory)
                            .Distinct()
                            .OrderBy(d => d.CategoryName)
                            .ToList();
                        return categories;
                    }
                    else
                    {
                        return new List<VisitCategory>();
                    }
                }
                else
                {
                    return AllCategories;
                }
            }
        }
        public List<VisitCategory> AllCategories { get; internal set; }
        public List<MedicalWorker> AllMedicalWorkers { get; internal set; }
        public List<MedicalService> AllMedicalServices { get; internal set; }
        public List<Location> AllLocations { get; internal set; }
        public Patient SelectedPatient { get; set; }
        public string UserName 
        {
            get ; 
            set ; 
        }

        private List<Visit> FilterVisits()
        {
            IQueryable<Visit> filteredVisits = AllVisitsList;
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
                            filteredVisits = filteredVisits.Where(c => c.MedicalWorker.Id == lid);
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
                        filteredVisits = filteredVisits.Where(c => c.Location.Id == lid);
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
                        filteredVisits = filteredVisits.Where(c => c.PrimaryService.Id == lid);
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
                        filteredVisits = filteredVisits.Where(c => c.VisitCategory.Id == int.Parse(SelectedCategoryId));
                        if (filteredVisits == null)
                        {
                            return null;
                        }
                    }                    
                }
            }
            filteredVisits = filteredVisits.OrderBy(c => c.DateTimeSince);
            if (filteredVisits.Count()<ItemsPerPage)
            {
                return filteredVisits.ToList();
            }
            else
            {
                return filteredVisits.Skip((CurrentPageNum-1)*ItemsPerPage).Take(ItemsPerPage).ToList();
            }           
        }
    }
}

