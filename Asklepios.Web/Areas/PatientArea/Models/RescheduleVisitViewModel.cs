using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public class RescheduleVisitViewModel: IBaseViewModel
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
                //if (_rescheduledVisit==null)
                //{
                //    _rescheduledVisit =AllVisitsList.FirstOrDefault(c => c.Id == RescheduledVisitId);
                //}
                return _rescheduledVisit;
            }
            set 
            { 
                _rescheduledVisit = value; 
            }
        }
        public int CurrentPageNum { get; set; } 
        public int NumberOfPages
        {
            get
            {
                if (FilteredVisits==null)
                {
                    return 0;
                }
                return FilteredVisits.Count / ItemsPerPage;
            }
        }
        const int ItemsPerPage = 100;

        public bool NoReferral { get; set; }
        public string UserName { get; set; }

        public RescheduleVisitViewModel()
        {
            CurrentPageNum = 1;
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
                if (FilteredVisits == null)
                {
                    return null;
                }
                if (FilteredVisits.Count() > 0)
                {
                    List<Location> locations = FilteredVisits
                        .Select(c => c.Location)
                        .Distinct()
                        .OrderBy(d=>d.Name)
                        .ToList();
                    return locations;
                }
                else
                {
                    return new List<Location>();
                }
            }
        }
        public List<MedicalWorker> GetMedicalWorkers
        {
            get
            {
                if (FilteredVisits == null)
                {
                    return null;
                }

                if (FilteredVisits.Count() > 0)
                {
                    List<MedicalWorker> workers = FilteredVisits
                        .Where(c => c.PrimaryServiceId == RescheduledVisit.PrimaryServiceId)
                        .Select(e => e.MedicalWorker)
                        .Distinct()
                        .OrderBy(f=>f.FullProffesionalName)
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
                    List<MedicalService> services = AllVisitsList
                        .Select(c => c.PrimaryService)
                        .Distinct()
                        .OrderBy(d=>d.Name)
                        .ToList();
                    return services;
                }
                else
                {
                    return new List<MedicalService>();
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
                    List<VisitCategory> categories =  AllVisitsList
                        .Where(d=>d.VisitCategoryId==RescheduledVisit.VisitCategoryId)
                        .Select(c => c.VisitCategory)
                        .Distinct()
                        .OrderBy(e=>e.CategoryName)
                        .ToList();
                    return categories;
                }
                else
                {
                    return new List<VisitCategory>();
                }
            }
        }

        public IEnumerable<MedicalService> MedicalServices { get; internal set; }
        public List<Notification> Notifications { get; set; }

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
                            filteredVisits = filteredVisits
                                .Where(c => c.MedicalWorker.Id == lid)
                                .AsQueryable();
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
                        filteredVisits = filteredVisits
                            .Where(c => c.Location.Id == lid)
                            .AsQueryable();
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
                        filteredVisits = filteredVisits.Where(c => c.PrimaryService.Id == lid).AsQueryable();
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
                        filteredVisits = filteredVisits
                            .Where(c => c.VisitCategory.Id == int.Parse(SelectedVisitCategoryId))
                            .AsQueryable();
                        if (filteredVisits == null)
                        {
                            return null;
                        }
                    }                    
                }
            }
            filteredVisits = filteredVisits
                .OrderBy(c => c.DateTimeSince)
                .AsQueryable();
            if (filteredVisits.Count()<ItemsPerPage)
            {
                return filteredVisits.ToList();
            }
            else
            {
                return filteredVisits
                    .Skip((CurrentPageNum-1)*ItemsPerPage)
                    .Take( ItemsPerPage)
                    .ToList();
            }           
        }
    }
}

