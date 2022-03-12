using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class HistoricalVisitsViewModel
    {
        public List<Visit> HistoricalVisits { get; set; }
        //public List<Visit> 
        public  List<Visit> ForthcomingVisits(int number)
        {
            if (HistoricalVisits.Count<number)
            {
                return HistoricalVisits.OrderBy(c => c.DateTimeSince).ToList();
            }
            else
            {
                return HistoricalVisits.OrderBy(c => c.DateTimeSince).Take(number).ToList();
            }
        }
        

    }
}
