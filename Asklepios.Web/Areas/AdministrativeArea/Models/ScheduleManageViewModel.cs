using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class ScheduleManageViewModel
    {
        public List<Visit> Schedule { get; set; }
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


        public string SelectedLocationId { get; set; }
        public string SelectedMedicalWorkerId { get; set; }
        public string SelectedPrimaryServiceId { get; set; }
        public string SelectedVisitCategoryId { get; set; }
        public bool IsFilterOn
        {
            get
            {
                if (!string.IsNullOrWhiteSpace( SelectedLocationId))
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
                return false;
            }
        }

        public int ItemsPerPage { get; private set; } = 100;
        public int CurrentPageNum { get; private set; } = 1;

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
            filteredVisits = filteredVisits.OrderBy(c => c.DateTimeSince).ToList();
            if (filteredVisits.Count < ItemsPerPage)
            {
                return filteredVisits;
            }
            else
            {
                return filteredVisits.GetRange((CurrentPageNum-1)*ItemsPerPage, ItemsPerPage);
            }
        }

    }
}
