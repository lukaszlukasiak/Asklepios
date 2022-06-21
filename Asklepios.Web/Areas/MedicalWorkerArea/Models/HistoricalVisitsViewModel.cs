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

        public IEnumerable<IGrouping<DateTime, Visit>> HistoricalVisits 
        {
            get
            {
                if (MedicalWorker == null)
                {
                    return null;
                }

                IEnumerable<IGrouping<DateTime, Visit>> grouping = MedicalWorker.PastVisits.GroupBy(c => c.DateTimeSince.Date);
                grouping=grouping.OrderBy(c => c.Key);

                if (SelectedDate==null)
                {
                    int max = grouping.Count();//MedicalWorker.PastVisits.Count();
                    if (max>10)
                    {
                        max = 10;
                    }


                    return grouping.Take(max);//MedicalWorker.PastVisits.GetRange(0, max);
                }
                else
                {

                    return grouping;
                    //return MedicalWorker.PastVisits.Where(c => c.DateTimeSince.Date == SelectedDate.Value.Date).ToList();
                }               
            }
            
        }
        [DataType(DataType.Date)]
        [Display(Name = "Wybrany dzień")]
        public DateTime? SelectedDate { get; set; }//= DateTime.Now.Date;
        public string UserName { get; set; }
    }
}
