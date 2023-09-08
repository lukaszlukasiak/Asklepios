using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public class Recommendation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }
        [Required(ErrorMessage = "Proszę podać tytuł rekomendacji.")]
        [DataType(DataType.Text)]
        [Display(Name = "Tytuł")]

        public string Title { get; set; }
        [Required(ErrorMessage = "Proszę podać treść rekomendacji.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Treść rekomendacji")]

        public string Description { get; set; }
        public long? VisitId { get; set; }
        [ForeignKey("VisitId")]
        public Visit Visit { get; set; }

        public Recommendation MockClone(long newId)
        {
            Recommendation  recommendation=new Recommendation();
            recommendation.Id = newId;
            recommendation.Title = Title;
            recommendation.Visit = Visit;
            recommendation.VisitId = VisitId;
            recommendation.Description = Description;
            return recommendation;
        }
    }
}
