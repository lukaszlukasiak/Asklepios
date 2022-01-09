using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public class RateVisitViewModel
    {
        [Required(ErrorMessage = "Proszę wybrać ocenę")]
        public string AtmosphereRate { get; set;  }
        [Required(ErrorMessage = "Proszę wybrać ocenę")]
        public string CompetenceRate { get; set; }
        [Required(ErrorMessage = "Proszę wybrać ocenę")]
        public string OverallRate { get; set; }
        [Required(ErrorMessage = "Proszę wpisać krótki opis wrażeń z wizyty")]
        public string Description { get; set; }
        public MedicalWorker MedicalWorker { get; set; }
        public long VisitId { get; set; }

        public bool IsDataProper
        {
            get
            {
                VisitReview review = GetVisitReview();
                if (review.AtmosphereRate>0 && review.CompetenceRate>0 &&review.GeneralRate>0 && !string.IsNullOrWhiteSpace(review.ShortDescription))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        internal VisitReview GetVisitReview()
        {
            VisitReview visitReview = new VisitReview();
            visitReview.ReviewDate = DateTimeOffset.Now;
            visitReview.ShortDescription = Description;
            visitReview.AtmosphereRate = GetDigitValueFromTextValue(AtmosphereRate);
            visitReview.CompetenceRate = GetDigitValueFromTextValue(CompetenceRate);
            visitReview.GeneralRate = GetDigitValueFromTextValue(OverallRate);

            return visitReview;
        }

        private float GetDigitValueFromTextValue(string rate)
        {
            switch (rate)
            {
                case "Niedostateczna": return 1;
                case "Mierna": return 2;
                case "Dostateczna": return 3;
                case "Dobra": return 4;
                case "Bardzo dobra": return 5;
                default:           return -1;
            }
        }
    }
}
