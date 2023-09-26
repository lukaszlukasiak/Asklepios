using Asklepios.Core.Models;
using Asklepios.Web.Models;
using Asklepios.Web.ServiceClasses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public class BookVisitViewModel : SearchViewModel, IBaseViewModel
    {
        public IQueryable<Visit> PreFilteredVisitsList { get; set; }
        public List<Visit> _filteredVisits;
        public IQueryable<Visit> FilteredVisits;
        public ViewMessage ViewMessage { get; set; }
        public List<Visit> PageVisits { get; private set; }

        public int CurrentPageNum { get; set; } = 1;
        public int NumberOfPages
        {
            get
            {
                if (FilteredVisits == null)
                {
                    return -1;
                }
                int itemsNum = FilteredVisits.Count();
                if (itemsNum == 0)
                {
                    return -1;
                }
                int pagesNum = (int)Math.Ceiling((double)itemsNum / (double)PageSize);
                return pagesNum;
            }
        }
        const int PageSize = 100;

        public bool NoReferral { get; set; }

        public SelectList PagesList
        {
            get
            {
                List<PageSelect> items = new List<PageSelect>();
                for (int i = 1; i <= NumberOfPages; i++)
                {
                    PageSelect page = new PageSelect();
                    page.Value = i.ToString();
                    page.Id = i;
                    items.Add(page);
                }
                SelectList pagesList = new SelectList(items, "Id", "Value", CurrentPageNum);
                return pagesList;
            }
        }
        public bool HasAnyFilterSelected
        {
            get
            {
                return ((!string.IsNullOrWhiteSpace(SelectedCategoryId)) || (!string.IsNullOrWhiteSpace(SelectedLocationId)) || (!string.IsNullOrWhiteSpace(SelectedMedicalWorkerId)) || (!string.IsNullOrWhiteSpace(SelectedServiceId)));
            }
        }

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
                if (FilteredVisits == null || FilteredVisits.Count() == 0)
                {
                    return new List<Location>();
                }
                //if (HasAnythingPredefined)
                //{
                //    if (HasPredefinedLocation)
                //    {
                //        return AllLocations.Where(c => c.Id == long.Parse(SelectedLocationId)).ToList();
                //    }
                //    else
                //    {
                //        return FilteredVisits.Select(c => c.Location).Distinct().ToList();
                //    }
                //}
                //else
                //{
                //if (HasAnyFilterSelected && string.IsNullOrWhiteSpace(SelectedLocationId))
                //{
                //    List<Location> locations = PreFilteredVisitsList.Select(c => c.Location).Distinct().ToList();
                //    return locations;
                //}
                //else
                //{
                List<Location> locations = FilteredVisits.Select(c => c.Location).Distinct().ToList();
                return locations;
                //}
                //}
            }

        }
        public List<MedicalWorker> GetMedicalWorkers
        {
            get
            {
                if (FilteredVisits == null || FilteredVisits.Count() == 0)
                {
                    return new List<MedicalWorker>();
                }
                //if (HasAnythingPredefined)
                //{
                //    if (HasPredefinedMedicalWorker)
                //    {
                //        List<MedicalWorker> workers = AllMedicalWorkers.Where(c => c.Id == long.Parse(SelectedMedicalWorkerId)).ToList();
                //        return workers;
                //    }
                //    else
                //    {
                //        return PreFilteredVisitsList.Select(c => c.MedicalWorker).Distinct().ToList();
                //    }
                //}
                //else
                //{
                //    if (HasAnyFilterSelected && string.IsNullOrWhiteSpace(SelectedMedicalWorkerId))
                //    {
                //        List<MedicalWorker> workers = PreFilteredVisitsList.Select(c => c.MedicalWorker).Distinct().ToList();
                //        return workers;
                //    }
                //    else
                //    {
                List<MedicalWorker> workers = FilteredVisits.Select(c => c.MedicalWorker).Distinct().ToList();
                return workers;
                //}
            }
        }
        public List<MedicalService> GetMedicalServices
        {
            get
            {
                if (FilteredVisits == null || FilteredVisits.Count() == 0)
                {
                    return new List<MedicalService>();
                }
                //if (HasAnythingPredefined)
                //{
                //    if (HasPredefinedService)
                //    {
                //        List<MedicalService> list = AllMedicalServices.Where(c => c.Id == long.Parse(SelectedServiceId)).ToList();
                //        return list;
                //    }
                //    else
                //    {
                //        return PreFilteredVisitsList.Select(c => c.PrimaryService).Distinct().ToList();
                //    }
                //}
                //else
                //{
                //    if (HasAnyFilterSelected && string.IsNullOrWhiteSpace(SelectedServiceId))
                //    {
                //        List<MedicalService> services = FilteredVisits.Select(c => c.PrimaryService).Distinct().ToList();
                //        return services;
                //    }
                //    else
                //    {
                List<MedicalService> services = FilteredVisits.Select(c => c.PrimaryService).Distinct().ToList();
                return services;
                //}
                //}
            }
        }
        public List<VisitCategory> GetVisitCategories
        {
            get
            {
                if (FilteredVisits == null || FilteredVisits.Count() == 0)
                {
                    return new List<VisitCategory>();
                }
                //if (HasAnythingPredefined)
                //{
                //    if (HasPredefinedCategory)
                //    {
                //        return AllCategories.Where(c => c.Id == long.Parse(SelectedCategoryId)).ToList();
                //    }
                //    else
                //    {
                //        return PreFilteredVisitsList.Select(c => c.VisitCategory).Distinct().ToList();
                //    }
                //}
                //else
                //{
                //    if (HasAnyFilterSelected && string.IsNullOrWhiteSpace(SelectedCategoryId))
                //    {
                //        List<VisitCategory> categories = FilteredVisits.Select(c => c.VisitCategory).Distinct().ToList();
                //        return categories;
                //    }
                //    else
                //    {
                        List<VisitCategory> categories = FilteredVisits.Select(c => c.VisitCategory).Distinct().ToList();
                        return categories;
                //    }
                //}
            }
        }
        public List<VisitCategory> AllCategories { get; internal set; }
        public List<MedicalWorker> AllMedicalWorkers { get; internal set; }
        public List<MedicalService> AllMedicalServices { get; internal set; }
        public List<Location> AllLocations { get; internal set; }
        public Patient SelectedPatient { get; set; }
        public string UserName
        {
            get;
            set;
        }
        public void FilterVisits(IQueryable<Visit> allVisitsQuery)
        {
            IQueryable<Visit> filteredVisits = PreFilteredVisitsList;
            if (PreFilteredVisitsList == null)
            {
                return;
            }
            else
            {
                if (SelectedMedicalWorkerId != null)
                {
                    if (long.TryParse(SelectedMedicalWorkerId, out long lid))
                    {
                        if (lid > 0)
                        {
                            filteredVisits = filteredVisits.Where(c => c.MedicalWorker.Id == lid).AsQueryable();
                            if (filteredVisits == null)
                            {
                                return;
                            }
                        }
                    }
                }
            }
            if (SelectedLocationId != null)
            {
                if (long.TryParse(SelectedLocationId, out long lid))
                {
                    if (lid > 0)
                    {
                        filteredVisits = filteredVisits.Where(c => c.Location.Id == lid).AsQueryable();
                        if (filteredVisits == null)
                        {
                            return;
                        }
                    }

                }
            }
            if (SelectedServiceId != null)
            {
                if (long.TryParse(SelectedServiceId, out long lid))
                {
                    if (lid > 0)
                    {
                        filteredVisits = filteredVisits.Where(c => c.PrimaryService.Id == lid).AsQueryable();
                        if (filteredVisits == null)
                        {
                            return;
                        }
                    }

                }
            }
            if (SelectedCategoryId != null)
            {
                if (long.TryParse(SelectedCategoryId, out long lid))
                {
                    if (lid > 0)
                    {
                        filteredVisits = filteredVisits.Where(c => c.VisitCategory.Id == lid).AsQueryable();
                        if (filteredVisits == null)
                        {
                            return;
                        }
                    }
                }
            }

            FilteredVisits = filteredVisits.OrderBy(c => c.DateTimeSince).AsQueryable();

            List<Visit> pageVisits = Pagination.GetPageItems(CurrentPageNum, PageSize, FilteredVisits).ToList();
            PageVisits = pageVisits;
        }
    }
}

