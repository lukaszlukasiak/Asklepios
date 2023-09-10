using Asklepios.Core.Models;
using Asklepios.Web.Areas.AdministrativeArea.Interfaces;
using Asklepios.Web.Enums;
using Asklepios.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class VisitViewModel: ISearchVisit,IBaseViewModel
    {
        public Visit Visit { get; set; }
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
        public string UserName { get; set; }
        //public string Message { get; set; }
        //public AlertMessageType AlertMessageType { get; set; }
        public ViewMessage ViewMessage { get; set; } = new ViewMessage();
    }
}
