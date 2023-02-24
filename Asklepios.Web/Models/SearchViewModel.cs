using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Models
{
    public class SearchViewModel
    {
        public string SelectedVisitId { get; set; }
        public string SelectedCategoryId { get; set; }
        public string SelectedServiceId { get; set; }
        public string SelectedMedicalWorkerId { get; set; }
        public string SelectedLocationId { get; set; }

        public bool HasPredefinedMedicalWorker { get; set; }
        public bool HasPredefinedService { get; set; }
        public bool HasPredefinedCategory { get; set; }
        public bool HasPredefinedLocation { get; set; }

        public bool HasAnythingPredefined
        {
            get
            {
                return (HasPredefinedCategory || HasPredefinedLocation || HasPredefinedMedicalWorker || HasPredefinedService);
            }
        }

        public bool IsFilterActive 
        { 
            get
            {
                if (!string.IsNullOrEmpty(SelectedLocationId))
                {
                    return true;
                }
                if (!string.IsNullOrEmpty(SelectedMedicalWorkerId))
                {
                    return true;
                }
                if (!string.IsNullOrEmpty(SelectedCategoryId))
                {
                    return true;
                }
                if (!string.IsNullOrEmpty(SelectedServiceId))
                {
                    return true;
                }
                if (!string.IsNullOrEmpty(SelectedVisitId))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
