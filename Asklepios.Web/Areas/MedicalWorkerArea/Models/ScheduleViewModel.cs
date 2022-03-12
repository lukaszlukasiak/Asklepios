using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class ScheduleViewModel
    {
        public List<Visit> AllForthcomingVisits { get; set; }
        //public List<Visit> 
        public  List<Visit> ForthcomingVisits(int number)
        {
            if (AllForthcomingVisits.Count<number)
            {
                return AllForthcomingVisits.OrderBy(c => c.DateTimeSince).ToList();
            }
            else
            {
                return AllForthcomingVisits.OrderBy(c => c.DateTimeSince).Take(number).ToList();
            }
        }
        

    }
}
