using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class ReviewsViewModel
    {
        public ReviewsViewModel(MedicalWorker medicalWorker)
        {
            MedicalWorker = medicalWorker;
            VisitReviews = medicalWorker.VisitReviews?.OrderByDescending(c => c.ReviewDate).ToList();
        }

        public MedicalWorker MedicalWorker { get; set; }

        public List<VisitReview> VisitReviews { get; set; }
    }
}
