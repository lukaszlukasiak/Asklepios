using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public class RescheduleVisitSelectNewViewModel:IBaseViewModel
    {
        public long RescheduledVisitId { get; set; }
        public long SelectedNewVisitId { get; set; }
        public Visit SelectedNewVisit { get; set; }
        public Patient SelectedPatient { get; set; }
        public string UserName { get;set; }

        public RescheduleVisitSelectNewViewModel()
        {
            //SearchOptions = new VisitSearchOptions();

        }

    }
}

