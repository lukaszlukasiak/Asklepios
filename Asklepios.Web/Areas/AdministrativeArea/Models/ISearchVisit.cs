using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public interface ISearchVisit
    {
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
        public void SetSearchOptions(ISearchVisit iSearch)
        {
            ISearchVisit thisSearch = this;
            thisSearch.SelectedLocationId = iSearch.SelectedLocationId;
            thisSearch.SelectedMedicalRoomId = iSearch.SelectedMedicalRoomId;
            thisSearch.SelectedMedicalWorkerId = iSearch.SelectedMedicalWorkerId;
            thisSearch.SelectedPrimaryServiceId = iSearch.SelectedPrimaryServiceId;
            thisSearch.SelectedVisitCategoryId = iSearch.SelectedVisitCategoryId;
            thisSearch.VisitsDateFrom = iSearch.VisitsDateFrom;
            thisSearch.VisitsDateTo = iSearch.VisitsDateTo;
        }

    }
}
