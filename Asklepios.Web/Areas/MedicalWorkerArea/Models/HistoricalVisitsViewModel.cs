using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class HistoricalVisitsViewModel: IBaseViewModel
    {
        public MedicalWorker MedicalWorker { get; set; }


        public HistoricalVisitsViewModel(MedicalWorker medicalWorker)
        {
            MedicalWorker = medicalWorker;
        }
        public HistoricalVisitsViewModel()
        {
        }
        public IQueryable<Visit> HistoricalVisits { get; set; }
        public List<IGrouping<DateTime, Visit>> HistoricalVisitsGrouped 
        {
            get
            {
                if (MedicalWorker == null)
                {
                    return null;
                }

                if (SelectedDate==null)
                {
                    if (HistoricalVisits.Count() == 0 || HistoricalVisits == null)
                    {
                        return null;
                    }
                    List<Visit> visits = HistoricalVisits.ToList();

                    List<IGrouping<DateTime, Visit>> grouping = visits
                        .GroupBy(c => c.DateTimeSince.Date)
                        .OrderBy(e=>e.Key)
                        .ToList();

                    int max = grouping.Count();//MedicalWorker.PastVisits.Count();
                    if (max>10)
                    {
                        max = 10;
                    }

                    return grouping.Take(max).ToList();//MedicalWorker.PastVisits.GetRange(0, max);
                }
                else
                {
                    List<Visit> visits = HistoricalVisits
                        .Where(b => b.DateTimeSince.Date == SelectedDate.Value.Date)
                        .ToList();

                    if (visits==null)
                    {
                        return null;
                    }

                    List<IGrouping<DateTime, Visit>> grouping = visits
                        .GroupBy(c => c.DateTimeSince.Date)
                        .OrderBy(e=>e.Key)
                        .ToList();
                    return grouping;
                }               
            }           
        }
        [DataType(DataType.Date)]
        [Display(Name = "Wybrany dzień")]
        public DateTime? SelectedDate { get; set; }//= DateTime.Now.Date;
        public string UserName { get; set; }
    }
}
