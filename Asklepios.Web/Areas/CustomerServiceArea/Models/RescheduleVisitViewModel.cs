using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public class RescheduleVisitViewModel:IBaseViewModel
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
        public IQueryable<Visit> _serviceFilteredVisits;
        public IQueryable<Visit> ServiceFilteredVisits
        {
            get
            {
                if (_serviceFilteredVisits == null)
                {
                    _serviceFilteredVisits = FilterVisitsByService();
                }

                return _serviceFilteredVisits;
            }
        }

        //public VisitSearchOptions SearchOptions { get; set; } = new VisitSearchOptions();
        private string _selectedWorkerId;
        public string SelectedWorkerId
        {
            get
            {
                return _selectedWorkerId;
            }
            set
            {
                _selectedWorkerId = value;
            }
        }
        public string SelectedLocationId
        { get; set; }
        public string SelectedPrimaryServiceId
        { get; set; }
        public string SelectedVisitCategoryId
        { get; set; }
        public long RescheduledVisitId { get; set; }
        private Visit _rescheduledVisit;
        public Visit RescheduledVisit
        {
            get
            {
                return _rescheduledVisit;
            }
            set
            {
                _rescheduledVisit = value;
            }
        }

        public int CurrentPageNum { get; set; } = 1;
        public int NumberOfPages
        {
            get
            {
                if (FilteredVisits==null)
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

        public RescheduleVisitViewModel()
        {
            //SearchOptions = new VisitSearchOptions();

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
                if (AllVisitsList == null)
                {
                    return null;
                }
                if (AllVisitsList.Count() > 0)
                {
                    List<Location> locations = ServiceFilteredVisits
                        .Select(c => c.Location)
                        .Distinct()
                        .OrderBy(d => d.Name)
                        .ToList();
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
                if (AllVisitsList == null)
                {
                    return null;
                }

                if (AllVisitsList.Count() > 0)
                {
                    List<MedicalWorker> workers = ServiceFilteredVisits
                        .Where(c => c.PrimaryServiceId == RescheduledVisit.PrimaryServiceId)
                        .Select(e => e.MedicalWorker)
                        .Distinct()
                        .ToList();
                        
                    workers=workers
                        .OrderBy(f => f.FullProffesionalName)
                            //.ThenBy(g=>g.Person.Surname)
                              //  .ThenBy(h=>h.Person.Name)
                        .ToList();
                    return workers;
                }
                else
                {
                    return new List<MedicalWorker>();
                }
            }
        }
        public List<MedicalService> GetMedicalServicesAvailable
        {
            get
            {
                if (AllVisitsList == null)
                {
                    return null;
                }

                if (AllVisitsList.Count() > 0)
                {
                    List<MedicalService> services = ServiceFilteredVisits
                        .Select(c => c.PrimaryService)
                        .Distinct()
                        .OrderBy(d => d.Name)
                        .ToList();
                    return services;
                }
                else
                {
                    return null;
                }
            }
        }
        //public List<MedicalService> GetMedicalServicesAll
        //{
        //    get
        //    {
        //        if (AllVisitsList?.Count > 0)
        //        {
        //            List<MedicalService> services = ;
        //            return services;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        public List<VisitCategory> GetVisitCategories
        {
            get
            {
                if (AllVisitsList == null)
                {
                    return null;
                }

                if (AllVisitsList.Count() > 0)
                {
                    List<VisitCategory> categories = ServiceFilteredVisits
                        .Where(d => d.VisitCategoryId == RescheduledVisit.VisitCategoryId)
                        .Select(c => c.VisitCategory)
                        .Distinct()
                        .OrderBy(e => e.CategoryName)
                        .ToList();
                    return categories;
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<MedicalService> MedicalServices { get; internal set; }
        public Patient SelectedPatient { get; set; }
        public string UserName { get;set; }

        private IQueryable<Visit> FilterVisitsByService()
        {
            IQueryable<Visit> filteredVisits = AllVisitsList;
            if (AllVisitsList == null)
            {
                return null;
            }
            if (SelectedPrimaryServiceId != null)
            {
                if (long.TryParse(SelectedPrimaryServiceId, out long lid))
                {
                    if (lid > 0)
                    {
                        filteredVisits = filteredVisits.Where(c => c.PrimaryService.Id == lid);
                        if (filteredVisits == null)
                        {
                            return null;
                        }
                    }

                }
            }
            return filteredVisits;
        }
        private List<Visit> FilterVisits()
        {
            //if (SearchOptions!=null)
            //{
            //    return AllVisitsList;
            //}
            IQueryable<Visit> filteredVisits = AllVisitsList;
            if (AllVisitsList == null)
            {
                return null;
            }
            else
            {
                if (SelectedWorkerId != null)
                {
                    if (long.TryParse(SelectedWorkerId, out long lid))
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
            if (SelectedPrimaryServiceId != null)
            {
                if (long.TryParse(SelectedPrimaryServiceId, out long lid))
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
            if (SelectedVisitCategoryId != null)
            {
                if (long.TryParse(SelectedVisitCategoryId, out long lid))
                {
                    if (lid>0)
                    {
                        filteredVisits = filteredVisits.Where(c => c.VisitCategory.Id == int.Parse(SelectedVisitCategoryId));
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

