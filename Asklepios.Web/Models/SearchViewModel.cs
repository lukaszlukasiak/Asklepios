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
    }
}
