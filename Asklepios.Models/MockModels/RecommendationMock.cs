using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.MockModels
{
    public  class RecommendationMock
    {
        [Display(Name = "Tytuł rekomendacji")]
        [Required(ErrorMessage = "Proszę wprowadzić tytuł rekomendacji")]
        public string Title { get; set; }
        [Display(Name = "Treść rekomendacji")]
        [Required(ErrorMessage = "Proszę wprowadzić treść rekomendacji")]
        public string Description { get; set; }

        public bool IsModelValid 
        {   
            get
            {
                if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Description))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public Recommendation RecommendationFromMock()
        {
            Recommendation recommendation = new Recommendation();
            recommendation.Title = Title;
            recommendation.Description = Description;
            return recommendation;
        }
    }
}
