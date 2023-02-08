using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class ScheduleViewModel: IBaseViewModel
    {
        public MedicalWorker MedicalWorker { get; set; }
        public IQueryable<Visit> AllForthcomingVisits { get; set; }
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
                if (AllForthcomingVisits==null)
                {
                    return null;
                }
                if (AllForthcomingVisits.Count()==0)
                {
                    return null;
                }

                if (SelectedDate == null)
                {
                    List<Visit> visits = AllForthcomingVisits.ToList();
                    List<IGrouping<DateTime, Visit>> grouping = visits.GroupBy(c => c.DateTimeSince.Date).ToList();
                    grouping = grouping.OrderBy(c => c.Key).ToList();

                    int max = grouping.Count();//MedicalWorker.PastVisits.Count();
                    if (max > 5)
                    {
                        max = 5;
                    }


                    return grouping.Take(max);//MedicalWorker.PastVisits.GetRange(0, max);
                }
                else
                {
                    List<Visit> visits = AllForthcomingVisits.Where(d=>d.DateTimeSince.Date==SelectedDate.Value.Date).ToList();
                    List<IGrouping<DateTime, Visit>> grouping = visits.GroupBy(c => c.DateTimeSince.Date).ToList();
                    grouping = grouping.OrderBy(c => c.Key).ToList();

                    return grouping;
                    //return MedicalWorker.PastVisits.Where(c => c.DateTimeSince.Date == SelectedDate.Value.Date).ToList();
                }
            }

        }

        public string UserName { get ; set; }

        public      ScheduleViewModel(MedicalWorker medicalWorker)
        {
            MedicalWorker = medicalWorker;
        }
        public ScheduleViewModel()
        {
        }


    }
}
