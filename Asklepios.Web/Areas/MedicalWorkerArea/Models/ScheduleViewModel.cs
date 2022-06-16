using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class ScheduleViewModel
    {
        public MedicalWorker MedicalWorker { get; set; }
        //public List<Visit> AllForthcomingVisits { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Wybrany dzień")]

        public DateTimeOffset? SelectedDate { get; set; }
        //public  List<Visit> ForthcomingVisits(int number)
        //{
        //    if (MedicalWorker.FutureVisits.Count<number)
        //    {
        //        return MedicalWorker.FutureVisits.OrderBy(c => c.DateTimeSince).ToList();
        //    }
        //    else
        //    {
        //        return MedicalWorker.FutureVisits.OrderBy(c => c.DateTimeSince).Take(number).ToList();
        //    }
        //}
        public IEnumerable<IGrouping<DateTime, Visit>> ForthcomingVisits
        {
            get
            {
                if (MedicalWorker == null)
                {
                    return null;
                }

                IEnumerable<IGrouping<DateTime, Visit>> grouping = MedicalWorker.FutureVisits.GroupBy(c => c.DateTimeSince.Date);
                grouping = grouping.OrderBy(c => c.Key);

                if (SelectedDate == null)
                {
                    int max = grouping.Count();//MedicalWorker.PastVisits.Count();
                    if (max > 5)
                    {
                        max = 5;
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
        public      ScheduleViewModel(MedicalWorker medicalWorker)
        {
            MedicalWorker = medicalWorker;
        }
        public ScheduleViewModel()
        {
        }


    }
}
