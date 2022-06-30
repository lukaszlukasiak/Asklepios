using Asklepios.Core.Models;
using Asklepios.Web.Areas.AdministrativeArea.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class ScheduleManageViewModel : ISearchVisit, IBaseViewModel
    {
        public List<Visit> Schedule { get; set; }
        public ViewMode ViewMode { get; set; }
        public List<MedicalRoom> MedicalRooms { get; set; }
        public List<MedicalWorker> MedicalWorkers { get; set; }
        public List<MedicalService> PrimaryMedicalServices { get; set; }
        public List<Location> Locations { get; set; }
        public List<VisitCategory> VisitCategories { get; set; }
        public MedicalWorker SelectedMedicalWorker { get; set; }
        public List<Visit> FilteredSchedule
        {
            get
            {
                if (Schedule == null)
                {
                    return null;
                }
                if (IsFilterOn)
                {
                    return GetFilteredSchedule();
                }
                else
                {
                    if (Schedule.Count < ItemsPerPage)
                    {
                        return Schedule;
                    }
                    else
                    {
                        if (CurrentPageNumId<=0)
                        {
                            CurrentPageNumId = 1;
                        }
                        return Schedule.GetRange(((int)CurrentPageNumId - 1) * ItemsPerPage, ItemsPerPage);
                    }
                }
            }
        }
        public int NumberOfPages
        {
            get
            {
                if (Schedule == null)
                {
                    return -1;
                }
                int itemsNum = Schedule.Count;
                if (itemsNum == 0)
                {
                    return -1;
                }
                int pagesNum = (int)Math.Ceiling((double)itemsNum / (double)ItemsPerPage);
                return pagesNum;
            }
        }
        public List<int> PageNums
        {
            get
            {
                if (Schedule == null)
                {
                    return null;
                }
                int itemsNum = Schedule.Count;
                if (itemsNum == 0)
                {
                    return null;
                }
                int pagesNum = (int)Math.Ceiling((double)itemsNum / (double)ItemsPerPage);
                List<int> pages = new List<int>();


                for (int i = 1; i < pagesNum; i++)
                {
                    pages.Add(i);
                }
                return pages;
            }
        }
        //private DateTimeOffset? _firstVisitInitialDateTime;
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

        public int ItemsPerPage { get; private set; } = 100;
        [Display(Name = "Numer strony z wynikami")]

        public long CurrentPageNumId { get;  set; } 
        public string UserName { get; set; }

        //public void SetSearchOptions(ISearchVisit iSearch)
        //{
        //    ISearchVisit thisSearch = this;
        //    thisSearch.SelectedLocationId = iSearch.SelectedLocationId;
        //    thisSearch.SelectedMedicalRoomId = iSearch.SelectedMedicalRoomId;
        //    thisSearch.SelectedMedicalWorkerId = iSearch.SelectedMedicalWorkerId;
        //    thisSearch.SelectedPrimaryServiceId = iSearch.SelectedPrimaryServiceId;
        //    thisSearch.SelectedVisitCategoryId = iSearch.SelectedVisitCategoryId;
        //    thisSearch.VisitsDateFrom = iSearch.VisitsDateFrom;
        //    thisSearch.VisitsDateTo = iSearch.VisitsDateTo;
        //}

        private List<Visit> GetFilteredSchedule()
        {
            List<Visit> filteredVisits = Schedule;
            if (Schedule == null)
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
                    if (lid > 0)
                    {
                        filteredVisits = filteredVisits.Where(c => c.Location.Id == lid).ToList();
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
                        filteredVisits = filteredVisits.Where(c => c.MedicalRoom.Id == lid).ToList();
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
                        filteredVisits = filteredVisits.Where(c => c.PrimaryService.Id == lid).ToList();
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
                        filteredVisits = filteredVisits.Where(c => c.VisitCategory.Id == int.Parse(SelectedVisitCategoryId)).ToList();
                        if (filteredVisits == null)
                        {
                            return null;
                        }
                    }
                }
            }
            if (VisitsDateFrom.HasValue)
            {
                filteredVisits = filteredVisits.Where(c => c.DateTimeSince >= VisitsDateFrom).ToList();
                if (filteredVisits == null)
                {
                    return null;
                }

            }
            if (VisitsDateTo.HasValue)
            {
                filteredVisits = filteredVisits.Where(c => c.DateTimeSince <= VisitsDateTo.Value.AddDays(1)).ToList();
                if (filteredVisits == null)
                {
                    return null;
                }
            }
            if (IsBooked.HasValue)
            {
                if (IsBooked.Value)
                {
                    filteredVisits = filteredVisits.Where(c => c.VisitStatus == Core.Enums.VisitStatus.Booked).ToList();
                }
                else
                {
                    filteredVisits = filteredVisits.Where(c => c.VisitStatus == Core.Enums.VisitStatus.AvailableNotBooked).ToList();
                }
                if (filteredVisits == null)
                {
                    return null;
                }
            }

            filteredVisits = filteredVisits.OrderBy(c => c.DateTimeSince).ToList();
            if (filteredVisits.Count < ItemsPerPage)
            {
                return filteredVisits;
            }
            else
            {
                if (CurrentPageNumId ==0)
                {
                    CurrentPageNumId = 1;
                }
                if (CurrentPageNumId* ItemsPerPage>filteredVisits.Count)
                {
                    int lastPage = (int)Math.Ceiling(filteredVisits.Count / (ItemsPerPage+0.0));
                    int itemsNumber = filteredVisits.Count - (lastPage - 1) * ItemsPerPage;
                    return filteredVisits.GetRange(((lastPage-1) * ItemsPerPage), itemsNumber);

                }
                return filteredVisits.GetRange(((int)CurrentPageNumId - 1) * ItemsPerPage, ItemsPerPage);
            }
        }

    }
}
