using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public class Recommendation
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Proszę podać tytuł rekomendacji.")]
        [DataType(DataType.Text)]
        [Display(Name = "Tytuł")]

        public string Title { get; set; }
        [Required(ErrorMessage = "Proszę podać treść rekomendacji.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Treść rekomendacji")]

        public string Description { get; set; }
        public Visit Visit { get; set; }
        public long VisitId { get; set; }
        //public VisitSummary VisitSummary { get; set; }

    }
}
