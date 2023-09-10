using Asklepios.Core.Models;
using Asklepios.Web.Areas.AdministrativeArea.Interfaces;
using Asklepios.Web.Enums;
using Asklepios.Web.Models;
using Asklepios.Web.ServiceClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class ScheduleManageViewModel : ISearchVisit, IBaseViewModel
    {
        const int PageSize = 100;
        [Display(Name = "Numer strony z wynikami")]

        public int CurrentPageNum { get; set; } 

        public ViewMode ViewMode { get; set; }
        public List<MedicalRoom> MedicalRooms { get; set; }
        public List<MedicalWorker> MedicalWorkers { get; set; }
        public List<MedicalService> PrimaryMedicalServices { get; set; }
        public List<Location> Locations { get; set; }
        public List<VisitCategory> VisitCategories { get; set; }
        public MedicalWorker SelectedMedicalWorker { get; set; }

        public IQueryable<Visit> FilteredVisits { get; private set; }
        public List<Visit> PageVisits { get; private set; }

        public void FilterSchedule(IQueryable<Visit> allVisitsQuery)
        {
            
            if (allVisitsQuery == null)
            {
                return ;
            }
            if (IsFilterOn)
            {
                allVisitsQuery=GetFilteredSchedule(allVisitsQuery);
            }
            FilteredVisits = allVisitsQuery;

            List<Visit> pageVisits = Pagination.GetPageItems(CurrentPageNum, PageSize, allVisitsQuery).ToList();

            PageVisits = pageVisits;
            
        }
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

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [Display(Name = "Data od")]
        public DateTimeOffset? VisitsDateFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [Display(Name = "Data do")]
        public DateTimeOffset? VisitsDateTo { get; set; }

        [Display(Name = "Placówka medyczna")]

        public string SelectedLocationId { get; set; }
        [Display(Name = "Pokój medyczny")]
        public string SelectedMedicalRoomId { get; set; }
        [Display(Name = "Pracownik medyczny")]

        public string SelectedMedicalWorkerId { get; set; }
        [Display(Name = "Usługa medyczna")]

        public string SelectedPrimaryServiceId { get; set; }
        [Display(Name = "Kategoria wizyty")]

        public string SelectedVisitCategoryId { get; set; }

        [Display(Name = "Czy wizyta została zarezerwowana?")]

        public bool? IsBooked { get; set; }



        public bool IsFilterOn
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(SelectedLocationId))
                {
                    return true;
                }
                if (!string.IsNullOrWhiteSpace(SelectedVisitCategoryId))
                {
                    return true;
                }
                if (!string.IsNullOrWhiteSpace(SelectedMedicalWorkerId))
                {
                    return true;
                }
                if (!string.IsNullOrWhiteSpace(SelectedPrimaryServiceId))
                {
                    return true;
                }
                if (VisitsDateFrom.HasValue)
                {
                    return true;
                }
                if (VisitsDateTo.HasValue)
                {
                    return true;
                }
                if (IsBooked.HasValue)
                {
                    return true;
                }
                return false;
            }
        }

        public string UserName { get; set; }
        //public string Message { get; set; }

        //public AlertMessageType AlertMessageType { get; set; }
        public ViewMessage ViewMessage { get; set; } = new ViewMessage();

        public IQueryable<Visit> GetFilteredSchedule(IQueryable<Visit> allVisits)
        {

            IQueryable<Visit> filteredVisits = allVisits;//     Schedule;
            //if (Schedule == null)
            //{
            //    return null;
            //}
            if (allVisits == null)
            {
                return null;
            }

            else
            {
                if (SelectedMedicalWorkerId != null)
                {
                    if (long.TryParse(SelectedMedicalWorkerId, out long lid))
                    {
                        if (lid > 0)
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
                    if (lid > 0)
                    {
                        filteredVisits = filteredVisits.Where(c => c.Location.Id == lid);
                        if (filteredVisits == null)
                        {
                            return null;
                        }
                    }
                }
            }
            if (SelectedMedicalRoomId != null)
            {
                if (long.TryParse(SelectedMedicalRoomId, out long lid))
                {
                    if (lid > 0)
                    {
                        filteredVisits = filteredVisits.Where(c => c.MedicalRoom.Id == lid);
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
            if (SelectedVisitCategoryId != null)
            {
                if (long.TryParse(SelectedVisitCategoryId, out long lid))
                {
                    if (lid > 0)
                    {
                        filteredVisits = filteredVisits.Where(c => c.VisitCategory.Id == int.Parse(SelectedVisitCategoryId));
                        if (filteredVisits == null)
                        {
                            return null;
                        }
                    }
                }
            }
            if (VisitsDateFrom.HasValue)
            {
                filteredVisits = filteredVisits.Where(c => c.DateTimeSince >= VisitsDateFrom);
                if (filteredVisits == null)
                {
                    return null;
                }
            }
            if (VisitsDateTo.HasValue)
            {
                filteredVisits = filteredVisits.Where(c => c.DateTimeSince <= VisitsDateTo.Value.AddDays(1));
                if (filteredVisits == null)
                {
                    return null;
                }
            }
            if (IsBooked.HasValue)
            {
                if (IsBooked.Value)
                {
                    filteredVisits = filteredVisits.Where(c => c.VisitStatus == Core.Enums.VisitStatus.Booked);
                }
                else
                {
                    filteredVisits = filteredVisits.Where(c => c.VisitStatus == Core.Enums.VisitStatus.AvailableNotBooked);
                }
                if (filteredVisits == null)
                {
                    return null;
                }
            }

            filteredVisits = filteredVisits.OrderBy(c => c.DateTimeSince);

            return filteredVisits;

        }
    }
}
