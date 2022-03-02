using Asklepios.Core.Models;
using Asklepios.Web.Areas.AdministrativeArea.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class ScheduleManageViewModel:ISearchVisit
    {
        public List<Visit> Schedule { get; set; }
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
                if (IsFilterOn)
                {
                    return GetFilteredSchedule();
                }
                else
                {
                    return Schedule;
                }
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
        public int CurrentPageNum { get; private set; } = 1;

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
                filteredVisits = filteredVisits.Where(c => c.DateTimeSince >=VisitsDateFrom).ToList();
                if (filteredVisits == null)
                {
                    return null;
                }

            }
            if (VisitsDateTo.HasValue)
            {
                filteredVisits = filteredVisits.Where(c => c.DateTimeSince <= VisitsDateTo.Value.AddDays(1)  ).ToList();
                if (filteredVisits == null)
                {
                    return null;
                }
            }
            if (IsBooked.HasValue)
            {
                filteredVisits = filteredVisits.Where(c => c.IsBooked== IsBooked.Value).ToList();
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
                return filteredVisits.GetRange((CurrentPageNum - 1) * ItemsPerPage, ItemsPerPage);
            }
        }

    }
}
